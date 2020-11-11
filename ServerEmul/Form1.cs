﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerEmul
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Client에서 보낸 socket을 받아들일 리스너 선언
        // Client tcp open : server ip & server port 필요
        // server open : server port만 필요
        // TCP는 dedicated, 즉 '전속'적이다. = 양쪽 서버와 클라이언트중
        // 한쪽이 불통이면 TCP 통신은 먹통이 됨. 고로 전속적이다(전속계약)
        TcpListener _listen;

        byte[] Buffer = new byte[10000]; 

        private void btnStart_Click(object sender, EventArgs e)
        {
            _listen = new TcpListener(int.Parse(tbServerPort.Text));
            _listen.Start();    // client의 socket을 받아들일 준비 시작

            TcpClient _client = _listen.AcceptTcpClient();
            // _listen.AcceptTcpClient()로 받은 socket을 
            // TcpClient _client로 세션에 저장할수 있도록 선언.

            NetworkStream ns = _client.GetStream();
            // 파일스트림처럼 클라이언트와 서버를 연결시킬 통로 객체 ns 선언.
            ns.Read(Buffer, 0, 10000);
            // 담을 그릇 Buffer, 최소크기 0, 최대크기 10000
            string str = Encoding.Default.GetString(Buffer);
            // 클라이언트에서 string 데이터를 Stream(통로)에 전송하기위해
            // byte 데이터로 인코딩했다면 서버에서는 GetStream을 통해
            // Read()로 받은 byte데이터를 다시 string으로 인코딩해서
            // str문자객체에 담은 후 tbCommand창에 str을 뿌려준다. 
            tbCommand.Text += str + "\r\n";
        }
    }
}
