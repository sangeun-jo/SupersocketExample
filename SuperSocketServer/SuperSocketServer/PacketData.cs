using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSocketServer
{
    public class PacketData
    {
        public NetworkSession session;
        public EFBinaryRequestInfo reqInfo;
    }

    public enum PACKETID : int
    {
        REQ_ECHO = 1,
    }

    public class CommonHandler
    {
        //바이너리 패킷을 보냈던 곳으로 다시 보내기
        public void RequestEcho(NetworkSession session, EFBinaryRequestInfo requestInfo)
        {
            List<byte> dataSource = new List<byte>();
            dataSource.AddRange(BitConverter.GetBytes((int)PACKETID.REQ_ECHO));
            dataSource.AddRange(BitConverter.GetBytes(requestInfo.Body.Length));
            dataSource.AddRange(requestInfo.Body);

            session.Send(dataSource.ToArray(), 0, dataSource.Count);
        }
    }

    public class PK_ECHO
    {
        public string msg;
    }
}
