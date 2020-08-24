using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PassScanDLL
{
    public class PassScan
    {
        #region PassScanDLL Properties
        private int _errcode = 0;
        private int _timeout = 0;
        private string _portno = "";
        #endregion

        public SerialPort _serialPort = new System.IO.Ports.SerialPort();

        public delegate void receivedEventHandler(Object sender, receiveEventArgs e);
        public event receivedEventHandler CallReceivedData;

        private System.Threading.Timer tmWaitTime;

        public PassScan()
        {
            tmWaitTime = new System.Threading.Timer(TimerCallback, null, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        }

        private void TimerCallback(object state)
        {
            tmWaitTime.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

            ScanCancel();

            _errcode = 0;

            receiveEventArgs eventArgs = new receiveEventArgs();
            eventArgs.MRZ1 = "";
            eventArgs.MRZ2 = "";
            CallReceivedData(new object(), eventArgs);
        }

        public bool Open()
        {
            bool confFlag = false;
            string PortNo = "";

            foreach (string portname in SerialPort.GetPortNames())
            {
                // Use your connection settings - own baud rate etc
                //MessageBox.Show(portname);

                SerialPort sp = new SerialPort(portname, 115200, Parity.None, 8, StopBits.One);
                try
                {
                    sp.ReadTimeout = 500 * 1;
                    sp.WriteTimeout = 500 * 1;
                    sp.Open();

                    byte[] sndbuf = new byte[3];
                    byte[] rcvbuf = new byte[3];

                    sndbuf[0] = 0x03;
                    sndbuf[1] = 0x4D;  //'M'
                    sndbuf[2] = 0x56;  //'V'

                    sp.Write(sndbuf, 0, 3);
                    Thread.Sleep(1000);

                    sp.Read(rcvbuf, 0, 3);

                    if (rcvbuf[0] == 0x03 && rcvbuf[1] == 0x4D && rcvbuf[2] == 0x56)
                    {
                        //MessageBox.Show("Phone connected to: " + portname);
                        PortNo = portname;
                        confFlag = true;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("#1 : " + ex.ToString());
                }
                finally
                {
                    //MessageBox.Show("Port Closed: " + portname);
                    sp.Close();
                }
            }

            if (confFlag)
            {
                try
                {
                    _serialPort = new SerialPort(PortNo, 115200, Parity.None, 8, StopBits.One);
                    _serialPort.Encoding = Encoding.Default;
                    _serialPort.Handshake = Handshake.None;
                    _serialPort.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
                    _serialPort.ReadBufferSize = 1204;
                    _serialPort.ReadTimeout = 500 * 1;
                    _serialPort.WriteTimeout = 500 * 1;

                    if (!_serialPort.IsOpen)
                    {
                        _serialPort.Open();
                        _portno = PortNo;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("#1 : " + ex.ToString());

                    if (_serialPort.IsOpen)
                        _serialPort.Close();
                    return false;
                }
            }                
            return confFlag;
        }

        public bool Close()
        {
            try
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
            }
            catch { return false; }
            return true;
        }

        public bool Scan()
        {
            byte[] buf = new byte[6];

            buf[0] = 0x02;  //STX
            buf[1] = 0x00;  //size
            buf[2] = 0x02;  //size
            buf[3] = 0xA1;  //Command ID
            buf[4] = 0x03;  //ETX
            buf[5] = 0xA0;

            try
            {
                _serialPort.Write(buf, 0, 6);
                tmWaitTime.Change(1000 * _timeout, 1000 * _timeout);
            }
            catch { return false; }
            return true;
        }

        public bool ScanCancel()
        {
            byte[] buf = new byte[6];

            buf[0] = 0x02;  //STX
            buf[1] = 0x00;  //size
            buf[2] = 0x02;  //size
            buf[3] = 0xAC;  //Command ID
            buf[4] = 0x03;  //ETX
            for (int i = 1; i < 5; i++)
                buf[5] ^= buf[i];

            try
            {
                _serialPort.Write(buf, 0, buf.Length);

            }
            catch { return false; }
            return true;
        }

        private void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string MRZ1 = "";
            string MRZ2 = "";

            byte[] tmpBuf = new byte[1024];
            byte[] recvBuf = new byte[1024];

            Array.Clear(recvBuf, 0x00, recvBuf.Length);

            tmWaitTime.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

            int tmpCnt = 0;
            int readCnt = 0;
            int dataLen = 0;

            byte[] szMRZ1 = new byte[45];
            byte[] szMRZ2 = new byte[45];

            Array.Clear(szMRZ1, 0x00, szMRZ1.Length);
            Array.Clear(szMRZ2, 0x00, szMRZ2.Length);

            try
            {
                Array.Clear(tmpBuf, 0x00, tmpBuf.Length);
                if (_serialPort.BytesToRead >= 0)
                {
                    tmpCnt = _serialPort.Read(tmpBuf, 0, 1024);
                }

                Array.Copy(tmpBuf, 0, recvBuf, 0, tmpCnt);

                dataLen = tmpBuf[1] << 8 | tmpBuf[2];
                readCnt = tmpCnt;

                //MessageBox.Show(dataLen.ToString());

                if (dataLen + 4 != readCnt)
                {
                    //MessageBox.Show("readCnt :" + readCnt.ToString() + "         dataLen : " + (dataLen+4).ToString());

                    Array.Clear(tmpBuf, 0x00, tmpBuf.Length);
                    tmpCnt = _serialPort.Read(tmpBuf, 0, (dataLen + 4) - readCnt);

                    //MessageBox.Show("tmpCnt :" + tmpCnt.ToString());

                    Array.Copy(tmpBuf, 0, recvBuf, readCnt, tmpCnt);

                    readCnt += tmpCnt;
                }

                if (recvBuf[0] == 0x02 && recvBuf[readCnt - 2] == 0x03 &&
                    recvBuf[3] == 0xB1 && dataLen == 102)
                {
                    Array.Copy(recvBuf, 4, szMRZ1, 0, 44);
                    Array.Copy(recvBuf, 48, szMRZ2, 0, 44);


                    MRZ1 = Encoding.Default.GetString(szMRZ1, 0, szMRZ1.Length - 1);
                    MRZ2 = Encoding.Default.GetString(szMRZ2, 0, szMRZ2.Length - 1);

                    //MessageBox.Show(MSZ1 + MSZ2);

                    _errcode = 1;

                    receiveEventArgs eventArgs = new receiveEventArgs();
                    eventArgs.MRZ1 = MRZ1;
                    eventArgs.MRZ2 = MRZ2;
                    CallReceivedData(new object(), eventArgs);
                }
                else
                {
                    if (recvBuf[3] != 0xBC)
                    {
                        _errcode = -1;

                        receiveEventArgs eventArgs = new receiveEventArgs();
                        eventArgs.MRZ1 = "";
                        eventArgs.MRZ2 = "";
                        CallReceivedData(new object(), eventArgs);
                    }
                }

                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("#2 :" + ex.ToString());

                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
            }
        }

        #region Get/Set Properties
        public int timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }
        #endregion

        #region Get/Set Properties
        public int errcode
        {
            get { return _errcode; }
            set { _errcode = value; }
        }
        #endregion

        #region Get/Set Properties
        public string portno
        {
            get { return _portno; }
            set { _portno = value; }
        }
        #endregion
    }

    public class receiveEventArgs : EventArgs
    {
        #region receiveEventArgs Properties
        private string _mrz1 = null;
        private string _mrz2 = null;
        #endregion

        #region Get/Set Properties
        public string MRZ1
        {
            get { return _mrz1; }
            set { _mrz1 = value; }
        }

        public string MRZ2
        {
            get { return _mrz2; }
            set { _mrz2 = value; }
        }
        #endregion
    }
}
