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
        #region declare variable
        private AbortableBackgroundWorker backgroundWorker1;
        private AbortableBackgroundWorker backgroundWorker2;
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
        string tcpwin = "";
        string tcpsrc = "";
        string tcplen = "";
        string tcpdes = "";
        string udpscr = "";
        string udpdes = "";
        string httpheader = "";
        string httpbody = "";
        string httpver = "";
        string httplen = "";
        string reqres = "";
        string infor = "";
        //variables needed when saving files
        string folderName = "";
        // string pathString = "";

        int no = 0;
        #endregion



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

       
        #region hàm xử lý packet
        //ham xu ly offlive
        private void DispatcherHandler(Packet packet)
        {
            this.count = ""; this.time = ""; this.source = ""; this.destination = ""; this.protocol = ""; this.length = "";

            this.tcpack = ""; this.tcpsec = ""; this.tcpnsec = ""; this.tcpsrc = ""; this.tcpdes = ""; this.udpscr = "";

            this.udpdes = ""; this.httpheader = ""; this.httpver = ""; this.httplen = ""; this.reqres = ""; this.httpbody = "";

            this.infor = "";
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
                    tcpwin = tcp.Window.ToString();
                    tcplen = tcp.Length.ToString();
                    tcp.Length.ToString();
                    infor = tcpsrc + "->" + tcpdes + " Seq=" + tcpsec + " win=" + tcpwin + " Ack= " + tcpack + " LEN= " + tcplen;
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

            if (!count.Equals(""))
            {
                no++;
                ListViewItem item = new ListViewItem(no.ToString());
                item.SubItems.Add(time);
                item.SubItems.Add(source);
                item.SubItems.Add(destination);
                item.SubItems.Add(protocol);
                item.SubItems.Add(length);
                item.SubItems.Add(infor);
                item.Tag = packet;
                SetText(item);
            }
        }
        //hàm xử lý live
        private void PacketHandler(Packet packet)
        {

            this.count = ""; this.time = ""; this.source = ""; this.destination = ""; this.protocol = ""; this.length = "";

            this.tcpack = ""; this.tcpsec = ""; this.tcpnsec = ""; this.tcpsrc = ""; this.tcpdes = ""; this.udpscr = "";

            this.udpdes = ""; this.httpheader = ""; this.httpver = ""; this.httplen = ""; this.reqres = ""; this.httpbody = "";

            this.infor = "";
            if (no == 0)
            {
                InformationPacket(packet);
            }
            IpV4Datagram ip = packet.Ethernet.IpV4;
            TcpDatagram tcp = ip.Tcp;
            UdpDatagram udp = ip.Udp;
            HttpDatagram httpPacket = null;
            IcmpDatagram icmp = ip.Icmp;
            protocol = ip.Protocol.ToString();
            if (ip.Protocol.ToString().Equals("Tcp"))
            {
                httpPacket = tcp.Http;

                if ((httpPacket.Header != null))
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
                    tcpwin = tcp.Window.ToString();
                    tcplen = tcp.Length.ToString();
                    tcp.Length.ToString();
                    infor = tcpsrc + "->" + tcpdes + " Seq=" + tcpsec + " win=" + tcpwin + " Ack= " + tcpack + " LEN= " + tcplen;
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
                item.SubItems.Add(infor);
                item.Tag = packet;
                SetText(item);
            }


        }


        #endregion


        #region  xử lý  sự kiện

        #endregion

        private void InformationPacket(Packet packet)
        {
            TreeView tvPacket = new TreeView();
            //node frame
            TreeNode nodeframe = new TreeNode();
            nodeframe.Name = "nodeFrame";
            nodeframe.Text = "Frame";
            TreeNode nodeInterface = new TreeNode();
            nodeInterface.Name = "nodeInterface";
            nodeInterface.Text = "Interface Id: " + selectedAdapter.Name.Remove(0, 20);
            TreeNode nodeencap_typee = new TreeNode();
            nodeencap_typee.Name = "nodeencap_typee";
            nodeencap_typee.Text = "Encapsulation type: ";
            TreeNode nodeframenum = new TreeNode();
            nodeframenum.Name = "nodeframenum";
            nodeframenum.Text = "Frame Number: ";
            TreeNode nodeFrameLenght = new TreeNode();
            nodeFrameLenght.Name = "nodeFrameLenght";
            nodeFrameLenght.Text = "Frame Length: ";
            TreeNode nodeCaptureLenght = new TreeNode();
            nodeCaptureLenght.Name = "nodeCaptureLenght";
            nodeCaptureLenght.Text = "Capture Length: ";

            nodeframe.Nodes.Add(nodeInterface);
            nodeframe.Nodes.Add(nodeencap_typee);
            nodeframe.Nodes.Add(nodeframenum);
            nodeframe.Nodes.Add(nodeFrameLenght);
            nodeframe.Nodes.Add(nodeCaptureLenght);

            /// node enthernet
            TreeNode nodeEthernet = new TreeNode();
            nodeEthernet.Name = "nodeEthernet";
            nodeEthernet.Text = "Ethernet II: ";
            TreeNode nodepsDes = new TreeNode();
            nodepsDes.Name = "nodepsDes";
            nodepsDes.Text = "Destination: " + packet.Ethernet.Destination;
            TreeNode nodepsSrc = new TreeNode();
            nodepsSrc.Name = "nodepsDes";
            nodepsSrc.Text = "Source: " + packet.Ethernet.Source;
            nodeEthernet.Nodes.Add(nodepsDes);
            nodeEthernet.Nodes.Add(nodepsSrc);


            //node ipv4
            TreeNode nodeIPv4 = new TreeNode();
            nodeIPv4.Name = "nodeIPv4";
            nodeIPv4.Text = "Internet Protocol Version 4";
            TreeNode nodeVersion = new TreeNode();
            nodeVersion.Name = "nodeVersion";
            nodeVersion.Text = "Version: " +packet.Ethernet.Ip.Version.ToString();
            TreeNode nodeHeaderLeght = new TreeNode();
            nodeHeaderLeght.Name = "nodeHeaderLeght";
            nodeHeaderLeght.Text = "Header Lenght: " + packet.Ethernet.IpV4.HeaderLength;
            TreeNode nodeIdentification = new TreeNode();
            nodeIdentification.Name = "nodeIdentification";
            nodeIdentification.Text = "Identification: " + packet.Ethernet.IpV4.Identification;
            TreeNode nodeFlag = new TreeNode();
            nodeFlag.Name = "nodeFlag";
            nodeFlag.Text ="Flag: " ;
            TreeNode nodeFragment = new TreeNode();
            nodeFragment.Name = "nodeFragment";
            nodeFragment.Text = "Fragment offset: " + packet.Ethernet.IpV4.Fragmentation.Offset;
            TreeNode nodeTime = new TreeNode();
            nodeTime.Name = "nodeTime";
            nodeTime.Text = "Time to Live: " ;
            TreeNode nodeProtocol = new TreeNode();
            nodeProtocol.Name = "nodeProtocol";
            nodeProtocol.Text = "Protocol: " + packet.Ethernet.IpV4.Protocol;
            TreeNode nodeHeaderCheckSum = new TreeNode();
            nodeHeaderCheckSum.Name = "nodeHeaderCheckSum";
            nodeHeaderCheckSum.Text = "Header CheckSum: " + packet.Ethernet.IpV4.HeaderChecksum;
            TreeNode nodeipDes = new TreeNode();
            nodeipDes.Name = "nodeipDes";
            nodeipDes.Text = "Destination: " +packet.Ethernet.IpV4.Destination;
            TreeNode nodeipSource = new TreeNode();
            nodeipSource.Name = "nodeipSource";
            nodeipSource.Text = "Soucre: " + packet.Ethernet.IpV4.Source;
            nodeIPv4.Nodes.Add(nodeVersion);
            nodeIPv4.Nodes.Add(nodeHeaderLeght);
            nodeIPv4.Nodes.Add(nodeIdentification);
            nodeIPv4.Nodes.Add(nodeFlag);
            nodeIPv4.Nodes.Add(nodeFragment);
            nodeIPv4.Nodes.Add(nodeTime);
            nodeIPv4.Nodes.Add(nodeProtocol);
            nodeIPv4.Nodes.Add(nodeHeaderCheckSum);
            nodeIPv4.Nodes.Add(nodeipDes);
            nodeIPv4.Nodes.Add(nodeipSource);
            //add node
            tvPacket.Font = new Font("Times New Roman", 12, FontStyle.Regular);
            tvPacket.Nodes.Add(nodeframe);
            tvPacket.Nodes.Add(nodeEthernet);
            tvPacket.Nodes.Add(nodeIPv4);

            SetText2(tvPacket);

        }

        void AddTreeNode(string nodename, string nodetext, TreeNode nodeparent)
        {
            TreeNode node = new TreeNode();
            node.Name = nodename;
            node.Text = nodetext;
            nodeparent.Nodes.Add(node);
        }
        #region invoke 
        delegate void SetText2Callback(TreeView item);
        private void SetText2(TreeView item)
        {

            if (this.grBottom.InvokeRequired)
            {
                SetText2Callback d = new SetText2Callback(SetText2);
                this.Invoke(d, new object[] { item });
            }
            else
            {
                item.Dock = DockStyle.Top;
                grBottom.Controls.Add(item);
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
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (!count.Equals(""))
            {


                if (protocol.Equals("Tcp"))
                {
                    //  txtInForPacket.Text = "Protocol :  Tcp  \r\n SourcePort :  " + tcpsrc + "\r\n DestinationPort :  " + tcpdes + "\r\n SequenceNumber :  " + tcpsec + "\r\n NextSequenceNuber :  " + tcpnsec + "\r\n AcknowladgmentNumber :  " + tcpack;

                }
                else
                {
                    if (protocol.Equals("Http"))
                    {
                        //      txtInForPacket.Text = "Protocol :  Http  \r\n Version :  " + httpver + "\r\n Length :  " + httplen + "\r\n Type :  " + reqres + "\r\n Header :  \r\n" + httpheader + "\r\n Body :  \r\n" + httpbody;

                    }
                    else
                    {
                        if (protocol.Equals("Udp"))
                        {

                            //       txtInForPacket.Text = "Protocol :  Udp  \r\n SourcePort :  " + udpscr + "\r\n DestinationPort :  " + udpdes;

                        }
                    }
                }

            }

        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            if (this.backgroundWorker1.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            using (PacketCommunicator communicator = selectedAdapter.Open(65536, PacketDeviceOpenAttributes.Promiscuous, 1000))
            {

                if (communicator.DataLink.Kind != DataLinkKind.Ethernet)
                {
                    MessageBox.Show("This program works only on Ethernet networks!");

                    return;
                }


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

            }
            //}
        }




        ListViewItem data = new ListViewItem();


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
            if (listView1.SelectedItems.Count == 0)
                return;

            var item = listView1.SelectedItems[0];
            Packet myPacket = (Packet)item.Tag; //
            grBottom.Controls.Clear();
            InformationPacket(myPacket);
        }

        private void tbtnCapture_Click(object sender, EventArgs e)
        {
            if (adapters_list.SelectedIndex >= 0)
            {
                timer1.Enabled = true;
                timer1.Start();
                no = 0;
                listView1.Items.Clear();
                listView1.Update();
                grBottom.Controls.Clear();
                backgroundWorker1 = new AbortableBackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                backgroundWorker2 = new AbortableBackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
                backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
                selectedAdapter = AdaptersList[adapters_list.SelectedIndex];
                backgroundWorker1.RunWorkerAsync();
                backgroundWorker2.RunWorkerAsync();
                tbtnCapture.Enabled = false;
                //stop_button.Enabled = true;


            }
            else
            {
                MessageBox.Show("Chọn thiết bị mạng");
            }
        }

        private void tbtnPause_Click(object sender, EventArgs e)
        {

            ThreadWatcher.StopThread = true;//t
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
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


        private void tbtnStop_Click(object sender, EventArgs e)
        {

            ThreadWatcher.StopThread = true;//t
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
                backgroundWorker1.Abort();
                backgroundWorker1.Dispose();
            }
            if (backgroundWorker2.IsBusy)
            {
                backgroundWorker2.CancelAsync();
                backgroundWorker2.Abort();
                backgroundWorker2.Dispose();
            }
            no = 0;
            tbtnCapture.Enabled = true;
            timer1.Stop();
            listView1.Items.Clear();
            //   txtInForPacket.Clear();
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {


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

            DateTime currentTimestamp = statistics.Timestamp;


            DateTime previousTimestamp = _lastTimestamp;


            _lastTimestamp = currentTimestamp;


            if (previousTimestamp == DateTime.MinValue)
                return;


            double delayInSeconds = (currentTimestamp - previousTimestamp).TotalSeconds;


            double bitsPerSecond = statistics.AcceptedBytes * 8 / delayInSeconds;


            double packetsPerSecond = statistics.AcceptedPackets / delayInSeconds;
            MessageBox.Show(statistics.Timestamp + " BPS: " + bitsPerSecond + " PPS: " + packetsPerSecond);
            // Print timestamp and samples
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            selectedAdapter = AdaptersList[adapters_list.SelectedIndex];
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

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

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Browse Text Files";

            openFileDialog1.Filter = "dump files (*.pcap)|*.pcap";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                OfflinePacketDevice selectedDevice = new OfflinePacketDevice(openFileDialog1.FileName);
                using (PacketCommunicator communicator =
              selectedDevice.Open(65536,                                  // portion of the packet to capture
                                                                          // 65536 guarantees that the whole packet will be captured on all the link layers
                                  PacketDeviceOpenAttributes.Promiscuous, // promiscuous mode
                                  1000))                                  // read timeout
                {

                    communicator.ReceivePackets(0, DispatcherHandler);
                }
            }

        }


        private void tbtnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Browse Text Files";

            openFileDialog1.Filter = "dump files (*.pcap)|*.pcap";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                OfflinePacketDevice selectedDevice = new OfflinePacketDevice(openFileDialog1.FileName);
                using (PacketCommunicator communicator =
              selectedDevice.Open(65536,                                  // portion of the packet to capture
                                                                          // 65536 guarantees that the whole packet will be captured on all the link layers
                                  PacketDeviceOpenAttributes.Promiscuous, // promiscuous mode
                                  1000))                                  // read timeout
                {

                    communicator.ReceivePackets(0, DispatcherHandler);
                }
            }
        }





    }
    public static class ThreadWatcher
    {
        public static bool StopThread { get; set; }
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
