using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using PcapDotNet.Analysis;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Base;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Transport;
using PcapDotNet.Packets.Http;
using PcapDotNet.Packets.Icmp;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using MetroFramework.Forms;
using System.Threading;

namespace NSHW
{
    public partial class GUI : MetroForm
    {
        private IList<LivePacketDevice> AdaptersList;
        private PacketDevice selectedAdapter;
        private bool first_time = true; //boolean variable needed on re-capturing
        public static byte[] payload;
        public static Dictionary<int, Packet1> packets = new Dictionary<int, Packet1>();


        public GUI()
        {
            InitializeComponent();

            try
            {
                AdaptersList = LivePacketDevice.AllLocalMachine;//locate all adapters
            }
            catch (Exception e)
            {
                MessageBox.Show("Please make sure to run as Adminstrator and install Winpcap");
            }

            PcapDotNetAnalysis.OptIn = true;//enable pcap analysis

            if (AdaptersList.Count == 0)
            {

                MessageBox.Show("No adapters found !!");

                return;

            }

            //for (int i = 0; i != AdaptersList.Count; ++i)//add all adapters to my Combobox
            //{
            //    LivePacketDevice Adapter = AdaptersList[i];
            //    if (Adapter.Description != null)
            //    {
            //        ///   if(Adapter.==)
            //        adapters_list.Items.Add(Adapter.Description);


            //    }

            //    else
            //        adapters_list.Items.Add("Unknown");
            //}


            //System.Net.Sockets.IPPacketInformation ipPacket = new IPPacketInformation();

            //NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            //string s = adapters[ipPacket.Interface].Description;
            //for (int i = 0; i < adapters.Count(); i++)
            //{
            //    if (adapters[i].Supports(NetworkInterfaceComponent.IPv4))
            //    {
            //        adapters_list.Items.Add(adapters[i].GetPhysicalAddress().To);
            //    }
            //}

            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            int count = 0;
            for (int i = 0; i != AdaptersList.Count; ++i)
            {
                LivePacketDevice Adapter = AdaptersList[i];
                if (Adapter.Description != null)
                {

                    foreach (NetworkInterface adapter in adapters)
                    {

                        var ipProps = adapter.GetIPProperties();
                        IPInterfaceProperties properties = adapter.GetIPProperties();
                        if (adapter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                        {
                            string s = AdaptersList[count].Name.Remove(0, 20);
                            if (adapter.Id == AdaptersList[i].Name.Remove(0, 20))
                            {
                                adapters_list.Items.Add(adapter.Name);
                            }

                        }
                    }

                }

            }

        }


        //variabels needed to get info from packet
        string count = "";
        string time = "";
        string source = "";
        string destination = "";
        string protocol = "";
        string length = "";
        string tcpack = "";
        string tcpsec = "";
        string tcpnsec = "";
        string tcpsrc = "";
        string tcpdes = "";
        string udpscr = "";
        string udpdes = "";
        string httpheader = "";
        string httpbody = "";
        string httpver = "";
        string httplen = "";
        string reqres = "";
        //variables needed when saving files
        string folderName = "";
        // string pathString = "";

        int no = 0;
        private void PacketHandler(Packet packet)
        {
            this.count = ""; this.time = ""; this.source = ""; this.destination = ""; this.protocol = ""; this.length = "";

            this.tcpack = ""; this.tcpsec = ""; this.tcpnsec = ""; this.tcpsrc = ""; this.tcpdes = ""; this.udpscr = "";

            this.udpdes = ""; this.httpheader = ""; this.httpver = ""; this.httplen = ""; this.reqres = ""; this.httpbody = "";

            IpV4Datagram ip = packet.Ethernet.IpV4;
            TcpDatagram tcp = ip.Tcp;
            UdpDatagram udp = ip.Udp;
            HttpDatagram httpPacket = null;
            IcmpDatagram icmp = ip.Icmp;



            if (ip.Protocol.ToString().Equals("Tcp"))
            {
                httpPacket = tcp.Http;//Initialize http variable only if the packet was tcp

                if ((httpPacket.Header != null)/* && (!_tcp.Checked)*/)
                {
                    protocol = "Http";
                    httpheader = httpPacket.Header.ToString();
                    count = packet.Count.ToString();
                    time = packet.Timestamp.ToString();
                    this.source = ip.Source.ToString();
                    this.destination = ip.Destination.ToString();
                    length = ip.Length.ToString();
                    httpver = httpPacket.Version.ToString();
                    httplen = httpPacket.Length.ToString();
                    if ((httpPacket.Body != null)/* && (!_tcp.Checked)*/)
                    {
                        httpbody = httpPacket.Body.ToString();
                    }
                    if (httpPacket.IsRequest)
                    {
                        reqres = "Request";
                    }
                    else
                    {
                        reqres = "Response";
                    }
                }

                else
                {

                    count = packet.Count.ToString();
                    time = packet.Timestamp.ToString();
                    this.source = ip.Source.ToString();
                    this.destination = ip.Destination.ToString();
                    length = ip.Length.ToString();
                    protocol = ip.Protocol.ToString();
                    tcpsrc = tcp.SourcePort.ToString();
                    tcpdes = tcp.DestinationPort.ToString();
                    tcpack = tcp.AcknowledgmentNumber.ToString();
                    tcpsec = tcp.SequenceNumber.ToString();
                    tcpnsec = tcp.NextSequenceNumber.ToString();
                }


            }
            else
            {
                if (ip.Protocol.ToString().Equals("Udp"))
                {
                    if (udp.DestinationPort.ToString().Equals(53))
                    {
                        protocol = "dns";

                    }
                    else
                    {
                        count = packet.Count.ToString();
                        time = packet.Timestamp.ToString();
                        this.source = ip.Source.ToString();
                        this.destination = ip.Destination.ToString();
                        length = ip.Length.ToString();
                        protocol = ip.Protocol.ToString();
                        udpscr = udp.SourcePort.ToString();
                        udpdes = udp.DestinationPort.ToString();
                    }



                }

                else
                {

                    count = packet.Count.ToString();
                    time = packet.Timestamp.ToString();
                    this.source = ip.Source.ToString();
                    this.destination = ip.Destination.ToString();
                    length = ip.Length.ToString();
                    protocol = ip.Protocol.ToString();

                }
            }


            if (ip.Protocol.ToString().Equals("Tcp") /*&& (save.Checked)*/)
            {
                int _source = tcp.SourcePort;
                int _destination = tcp.DestinationPort;

                if (tcp.PayloadLength != 0) //not syn or ack
                {
                    payload = new byte[tcp.PayloadLength];
                    tcp.Payload.ToMemoryStream().Read(payload, 0, tcp.PayloadLength);// read payload from 0 to length
                    if (_destination == 80)// request from server
                    {
                        Packet1 packet1 = new Packet1();
                        if (payload.Count() > 1)
                        {
                            int i = Array.IndexOf(payload, (byte)32, 6);
                            byte[] t = new byte[i - 5];
                            Array.Copy(payload, 5, t, 0, i - 5);
                            packet1.Name = System.Text.ASCIIEncoding.ASCII.GetString(t);

                            if (!packets.ContainsKey(_source))
                                packets.Add(_source, packet1);

                        }
                    }
                    else
                        if (_source == 80)
                        if (packets.ContainsKey(_destination))
                        {
                            Packet1 packet1 = packets[_destination];
                            if (packet1.Data == null)
                            {
                                if ((httpPacket.Header != null) && (httpPacket.Header.ContentLength != null))
                                {
                                    packet1.Data = new byte[(uint)httpPacket.Header.ContentLength.ContentLength];
                                    Array.Copy(httpPacket.Body.ToMemoryStream().ToArray(), packet1.Data, httpPacket.Body.Length);
                                    packet1.Order = (uint)(tcp.SequenceNumber + payload.Length - httpPacket.Body.Length);
                                    packet1.Data_Length = httpPacket.Body.Length;
                                    for (int i = 0; i < packet1.TempPackets.Count; i++)
                                    {
                                        Temp tempPacket = packet1.TempPackets[i];
                                        Array.Copy(tempPacket.data, 0, packet1.Data, tempPacket.tempSeqNo - packet1.Order, tempPacket.data.Length);
                                        packet1.Data_Length += tempPacket.data.Length;
                                    }
                                }
                                else
                                {
                                    Temp tempPacket = new Temp();
                                    tempPacket.tempSeqNo = (uint)tcp.SequenceNumber;
                                    tempPacket.data = new byte[payload.Length];
                                    Array.Copy(payload, tempPacket.data, payload.Length);
                                    packet1.TempPackets.Add(tempPacket);
                                }
                            }
                            else if (packet1.Data_Length != packet1.Data.Length)
                            {
                                Array.Copy(payload, 0, packet1.Data, tcp.SequenceNumber - packet1.Order, payload.Length);

                                packet1.Data_Length += payload.Length;
                            }

                            //if (packet1.Data != null)
                            //    if (packet1.Data_Length == packet1.Data.Length)
                            //    {

                            //        using (BinaryWriter writer = new BinaryWriter(File.Open(@"D:\captured\" + Directory.CreateDirectory(Path.GetFileName(packet1.Name)), FileMode.Create)))
                            //        {
                            //            writer.Write(packet1.Data);

                            //        }

                            //        packets.Remove(_destination);

                            //    }
                        }
                }
            }
            if (!count.Equals(""))
            {
                no++;
                ListViewItem item = new ListViewItem(no.ToString());
                item.SubItems.Add(time);
                item.SubItems.Add(source);
                item.SubItems.Add(destination);
                item.SubItems.Add(protocol);
                item.SubItems.Add(length);
                SetText(item);
            }


        }
        delegate void SetTextCallback(ListViewItem item);
        private void SetText(ListViewItem item)
        {

            if (this.listView1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { item });
            }
            else
            {
                int count = this.listView1.Items.Count;

                this.listView1.Items.Insert(count, item);
            }


        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            if (!count.Equals(""))
            {


                if (protocol.Equals("Tcp"))
                {
                    txtInForPacket.Text = "Protocol :  Tcp  \r\n SourcePort :  " + tcpsrc + "\r\n DestinationPort :  " + tcpdes + "\r\n SequenceNumber :  " + tcpsec + "\r\n NextSequenceNuber :  " + tcpnsec + "\r\n AcknowladgmentNumber :  " + tcpack;

                }
                else
                {
                    if (protocol.Equals("Http"))
                    {
                        txtInForPacket.Text = "Protocol :  Http  \r\n Version :  " + httpver + "\r\n Length :  " + httplen + "\r\n Type :  " + reqres + "\r\n Header :  \r\n" + httpheader + "\r\n Body :  \r\n" + httpbody;

                    }
                    else
                    {
                        if (protocol.Equals("Udp"))
                        {

                            txtInForPacket.Text = "Protocol :  Udp  \r\n SourcePort :  " + udpscr + "\r\n DestinationPort :  " + udpdes;

                        }
                    }
                }

            }

        }
        PacketCommunicator communicator;

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            if (this.backgroundWorker1.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            // do stuff...
            using (PacketCommunicator communicator = selectedAdapter.Open(65536, PacketDeviceOpenAttributes.Promiscuous, 1000))
            {
                // Check the link layer.
                if (communicator.DataLink.Kind != DataLinkKind.Ethernet)
                {
                    MessageBox.Show("This program works only on Ethernet networks!");

                    return;
                }

                ////Deallocation is  necessary
                //if (_tcp.Checked && (!_udp.Checked))//just tcp
                //{
                //    using (BerkeleyPacketFilter filter = communicator.CreateFilter("tcp"))
                //    {
                //        // Set the filter
                //        communicator.SetFilter(filter);
                //    }
                //}
                //else if (_udp.Checked && !(_tcp.Checked))//just udp
                //{
                //    using (BerkeleyPacketFilter filter = communicator.CreateFilter("udp"))
                //    {
                //        // Set the filter
                //        communicator.SetFilter(filter);
                //    }
                //}
                //else if (_tcp.Checked && (_udp.Checked))//tcp and udp
                //{
                //    using (BerkeleyPacketFilter filter = communicator.CreateFilter("ip and udp"))
                //    {
                //        // Set the filter
                //        communicator.SetFilter(filter);
                //    }
                //}

                // Begin the capture
                communicator.ReceivePackets(0, PacketHandler);
                

            }

        }
        string path;
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            //if (save.Checked)
            //{
            using (PacketCommunicator communicator = selectedAdapter.Open(65536, PacketDeviceOpenAttributes.Promiscuous, 1000))
            {
                string folderName = @"D:\Temp";
                path = Path.Combine(folderName);
                System.IO.Directory.CreateDirectory(path);
                string fileName = System.IO.Path.GetRandomFileName();
                path = System.IO.Path.Combine(path, fileName);
                
                using (PacketDumpFile dumpFile = communicator.OpenDump(path))
                {

                    communicator.ReceivePackets(0, dumpFile.Dump);
                }

                //using (PacketDumpFile dumpFile = communicator.OpenDump(@"D:\PacketSniffer\save"))
                ////  using (OfflinePacketDevice dumpFile = new OfflinePacketDevice(@"E:\CSharp\Pcap\dumpFile.pcap");))
                //{
                //    // start the capture
                //    communicator.ReceivePackets(0, dumpFile.Dump);
                //}
            }
            //}
        }

        private void captureToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        ListViewItem data = new ListViewItem();

        //filter
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            using (PacketCommunicator communicator = selectedAdapter.Open(65536, PacketDeviceOpenAttributes.Promiscuous, 1000))
            {
                using (BerkeleyPacketFilter filter = communicator.CreateFilter(tbFilter.Text))
                {
                    // Set the filter
                    communicator.SetFilter(filter);
                }
            }
        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {

            var rs = listView1.Items.Cast<ListViewItem>().Where(x => x.SubItems[3].Text.ToLower().Contains(tbFilter.Text.ToLower().ToString())).ToList();

            listView1.Items.Clear();
            foreach (var r in rs)
                listView1.Items.Add(r);


        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tbtnCapture_Click(object sender, EventArgs e)
        {
            if (adapters_list.SelectedIndex >= 0)
            {
                timer1.Enabled = true;
                timer1.Start();
                listView1.Update();
                selectedAdapter = AdaptersList[adapters_list.SelectedIndex];
                backgroundWorker1.RunWorkerAsync();
                backgroundWorker2.RunWorkerAsync();
                tbtnCapture.Enabled = false;
                //stop_button.Enabled = true;
                adapters_list.Enabled = false;

            }
            else
            {
                MessageBox.Show("Please select an adapter!");
            }
        }

        private void tbtnPause_Click(object sender, EventArgs e)
        {
            ThreadWatcher.StopThread = true;//t
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.Abort();
                backgroundWorker1.Dispose();
                
            }
            if (backgroundWorker2.IsBusy)
            {
                backgroundWorker2.Abort();
                backgroundWorker2.Dispose();
            }
            saveToolStripMenuItem.Enabled = true;

        }
        public static class ThreadWatcher
        {
            public static bool StopThread { get; set; }
        }

        private void tbtnStop_Click(object sender, EventArgs e)
        {
            ThreadWatcher.StopThread = true;
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.Abort();
                backgroundWorker1.Dispose();
            }
            if (backgroundWorker2.IsBusy)
            {
                backgroundWorker2.Abort();
                backgroundWorker2.Dispose();
            }
            no = 0;
            tbtnCapture.Enabled = true;
            timer1.Stop();
            listView1.Items.Clear();
            txtInForPacket.Clear();
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (protocol.Equals("Tcp"))
            {
                txtInForPacket.Text = "Protocol :  Tcp  \r\n SourcePort :  " + tcpsrc + "\r\n DestinationPort :  " + tcpdes + "\r\n SequenceNumber :  " + tcpsec + "\r\n NextSequenceNuber :  " + tcpnsec + "\r\n AcknowladgmentNumber :  " + tcpack;
                //  //if (save.Checked)
                //  //{
                //      using (StreamWriter writer = new StreamWriter(@"D:\Desktop\Capture\TcpPacketsInfo.txt", true))
                //      {
                //          writer.Write("Protocol :  Tcp  \r\n SourcePort :  " + tcpsrc + "\r\n DestinationPort :  " + tcpdes + "\r\n SequenceNumber :  " + tcpsec + "\r\n NextSequenceNuber :  " + tcpnsec + "\r\n AcknowladgmentNumber :  " + tcpack + "\r\n --------------------------------------------- \r\n");
                //      }
                ////  }
            }
            else
            {
                if (protocol.Equals("Http"))
                {
                    txtInForPacket.Text = "Protocol :  Http  \r\n Version :  " + httpver + "\r\n Length :  " + httplen + "\r\n Type :  " + reqres + "\r\n Header :  \r\n" + httpheader + "\r\n Body :  \r\n" + httpbody;
                    //if (save.Checked)
                    //{
                    //    using (StreamWriter writer = new StreamWriter(@"D:\Desktop\Capture\HttpPacketsInfo.txt", true))
                    //    {
                    //        writer.Write("Protocol :  Http  \r\n Version :  " + httpver + "\r\n Length :  " + httplen + "\r\n Type :  " + reqres + "\r\n Header :  \r\n" + httpheader + "\r\n --------------------------------------------- \r\n");
                    //    }
                    //}
                }
                else
                {
                    if (protocol.Equals("Udp"))
                    {

                        txtInForPacket.Text = "Protocol :  Udp  \r\n SourcePort :  " + udpscr + "\r\n DestinationPort :  " + udpdes;
                        //if (save.Checked)
                        //{
                        //    using (StreamWriter writer = new StreamWriter(@"D:\Desktop\Capture\UdpPacketsInfo.txt", true))
                        //    {
                        //        writer.Write("Protocol :  Udp  \r\n SourcePort :  " + udpscr + "\r\n DestinationPort :  " + udpdes + "\r\n --------------------------------------------- \r\n");
                        //    }
                        //}
                    }
                }
            }
        }

        private void bmssToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedAdapter = AdaptersList[adapters_list.SelectedIndex];
            using (PacketCommunicator communicator = selectedAdapter.Open(65536, PacketDeviceOpenAttributes.Promiscuous, 1000))
            {
                // Check the link layer.
                if (communicator.DataLink.Kind != DataLinkKind.Ethernet)
                {
                    MessageBox.Show("This program works only on Ethernet networks!");

                    return;
                }
                communicator.SetFilter("tcp");
                communicator.Mode = PacketCommunicatorMode.Statistics;
                communicator.ReceiveStatistics(0, StatisticsHandler);
            }
        }
        private static DateTime _lastTimestamp;
        private static void StatisticsHandler(PacketSampleStatistics statistics)
        {
            // Current sample time
            DateTime currentTimestamp = statistics.Timestamp;

            // Previous sample time
            DateTime previousTimestamp = _lastTimestamp;

            // Set _lastTimestamp for the next iteration
            _lastTimestamp = currentTimestamp;

            // If there wasn't a previous sample than skip this iteration (it's the first iteration)
            if (previousTimestamp == DateTime.MinValue)
                return;

            // Calculate the delay from the last sample
            double delayInSeconds = (currentTimestamp - previousTimestamp).TotalSeconds;

            // Calculate bits per second
            double bitsPerSecond = statistics.AcceptedBytes * 8 / delayInSeconds;

            // Calculate packets per second
            double packetsPerSecond = statistics.AcceptedPackets / delayInSeconds;
            MessageBox.Show(statistics.Timestamp + " BPS: " + bitsPerSecond + " PPS: " + packetsPerSecond);
            // Print timestamp and samples
        }
        bool enableSave=false;
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
                selectedAdapter = AdaptersList[adapters_list.SelectedIndex];
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                saveFileDialog1.InitialDirectory = @"C:\";
                saveFileDialog1.Title = "Browse Text Files";
          
                saveFileDialog1.Filter = "dump files (*.pcap)|*.pcap";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.FileName = "unknown.pcap";
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName != "")
                {
                    FileInfo fi = new FileInfo(path);
                    fi.CopyTo(saveFileDialog1.FileName);
                }
           
        }
    }

    public class AbortableBackgroundWorker : BackgroundWorker
    {

        private Thread workerThread;

        protected override void OnDoWork(DoWorkEventArgs e)
        {
            workerThread = Thread.CurrentThread;
            try
            {
                base.OnDoWork(e);
            }
            catch (ThreadAbortException)
            {
                e.Cancel = true;
                Thread.ResetAbort();
            }
        }


        public void Abort()
        {
            if (workerThread != null)
            {
                workerThread.Abort();
                workerThread = null;
            }
        }
    }
}
