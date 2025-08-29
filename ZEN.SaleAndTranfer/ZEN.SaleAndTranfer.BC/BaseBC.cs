using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.ET.DDL;

namespace ZEN.SaleAndTranfer.BC
{
    public class BaseBC
    {
        public List<DDLItemET> GetPerPage()
        {
            List<DDLItemET> perPageList = new List<DDLItemET>() { 
                new DDLItemET() {ITEM_TEXT="5", ITEM_VALUE="5"},
                new DDLItemET() {ITEM_TEXT="10", ITEM_VALUE="10"},
                new DDLItemET() {ITEM_TEXT="20", ITEM_VALUE="20"},
                new DDLItemET() {ITEM_TEXT="50", ITEM_VALUE="50"},
                new DDLItemET() {ITEM_TEXT="100", ITEM_VALUE="100"},
                new DDLItemET() {ITEM_TEXT="500", ITEM_VALUE="500"},
                new DDLItemET() {ITEM_TEXT="1000", ITEM_VALUE="1000"}
                };
            return perPageList;
        }

        public bool IsFranchises(string brandCode, string branchCode)
        {
            try
            {
                var bc = new TB_M_BRANCH_BC();
                var result = bc.IsFranchises(branchCode, branchCode);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
