using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.AUT;

namespace ZEN.SaleAndTranfer.VM
{
    public class BaseVM
    {
        public List<MessageET> MessageList
        {
            get { return _messageList; }
        }

        private List<MessageET> _messageList;
        public PaginationVM ZenPagination
        {
            get
            {
                return _zenPagination;
            }
            set
            {
                _zenPagination = value;
            }
        }

        private PaginationVM _zenPagination;
        public BaseVM()
        {
            _messageList = new List<MessageET>();
        }
        public void AddMessageList(List<MessageET> msgList)
        {
            try
            {
                if (_messageList == null)
                    _messageList = new List<MessageET>();

                _messageList.AddRange(msgList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ClearMessageList()
        {
            try
            {
                _messageList = new List<MessageET>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AddMessage(MessageET msg)
        {
            try
            {
                if (_messageList == null)
                    _messageList = new List<MessageET>();

                _messageList.Add(msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UserLoginInfoET SessionLogin
        {
            get
            {
                return _sessionLogin;
            }
            set
            {
                _sessionLogin = value;
            }
        }

        private UserLoginInfoET _sessionLogin;
        public List<SortingInfoET> SortingInfoList
        {
            get { return _sortingInfoList; }
            set { _sortingInfoList = value; }
        }

        private List<SortingInfoET> _sortingInfoList;

        public string DT_SHORT_FORMAT
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["CULTER_INFO_ShortDatePattern_0"];
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public string SQL_MIN_DATE_TIME
        {
            get
            {
                try
                {
                    return "1/1/1753 00:00:00";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DateTime SQL_MIN_DATE_TIME_OBJ
        {
            get
            {
                try
                {
                    return new DateTime(1753, 1, 1);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public string FormMode { get; set; }  //FormMode ส่วนของการประเมิน SupaneeJ 20190124
    }
}
