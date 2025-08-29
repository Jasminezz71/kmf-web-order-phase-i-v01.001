using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZEN.SaleAndTranfer.DC.DDL;
using ZEN.SaleAndTranfer.DC.MAS;
using ZEN.SaleAndTranfer.ET;
using ZEN.SaleAndTranfer.ET.DDL;
using ZEN.SaleAndTranfer.ET.MAS;
using ZEN.SaleAndTranfer.VM.MAS;
using System.IO;
using System.Collections;
using ZEN.SaleAndTranfer.BC.CNF;
using ZEN.SaleAndTranfer.ET.CNF;
using ZEN.SaleAndTranfer.DC;

namespace ZEN.SaleAndTranfer.BC.MAS
{
    public class DeliveryScheduleBC : BaseBC
    {
        #region ---- Innitial ----

        public DeliveryScheduleVM InnitialCriteria(DeliveryScheduleVM vm)
        {
            try
            {
                if (vm.DSSearchCriteriaVM == null) { vm.DSSearchCriteriaVM = new DeliveryScheduleSearchCriteriaVM(); }
                var ddlDC = new DDLDC();
                List<DDLItemET> brandList = new List<DDLItemET>();
                var brandlist = ddlDC.GetBranchByUsername(DDLModeEnumET.SELECT_ALL, vm.SessionLogin.USER_NAME);
                var branchlist = new List<DDLItemET>();
                //var locationList = ddlDC.GetFCLocation(DDLModeEnumET.SELECT_ALL);
                //if (vm.DSSearchCriteriaVM.locationList == null) { vm.DSSearchCriteriaVM.locationList = new List<DDLItemET>(); }
                //vm.DSSearchCriteriaVM.locationList = locationList;

                if (vm.DSSearchCriteriaVM.BRAND_CODE != null && vm.SessionLogin.BRAND_CODE != "-- ทั้งหมด --")
                { branchlist = ddlDC.GetBranchbyBrand(vm.DSSearchCriteriaVM.BRAND_CODE, DDLModeEnumET.SELECT_ALL); }


                List<DDLItemET> zoneList = new List<DDLItemET>(); 
                zoneList.Add(new DDLItemET { ITEM_TEXT = "BKK", ITEM_VALUE = "BKK" });
                zoneList.Add(new DDLItemET { ITEM_TEXT = "Northeast", ITEM_VALUE = "Northeast" });
                zoneList.Add(new DDLItemET { ITEM_TEXT = "Central", ITEM_VALUE = "Central" });
                zoneList.Add(new DDLItemET { ITEM_TEXT = "East", ITEM_VALUE = "East" });
                zoneList.Add(new DDLItemET { ITEM_TEXT = "South", ITEM_VALUE = "South" });
                vm.DSSearchCriteriaVM.zoneList = new List<DDLItemET>();
                vm.DSSearchCriteriaVM.zoneList = zoneList;

                List<DDLItemET> typeList = new List<DDLItemET>();
                vm.DSSearchCriteriaVM.typeList = new List<DDLItemET>();
                typeList.Add(new DDLItemET { ITEM_TEXT = "UPC+0", ITEM_VALUE = "UPC+0" });
                typeList.Add(new DDLItemET { ITEM_TEXT = "UPC+1", ITEM_VALUE = "UPC+1" });
                vm.DSSearchCriteriaVM.typeList = new List<DDLItemET>();
                vm.DSSearchCriteriaVM.typeList = typeList;

                List<DDLItemET> locationList = new List<DDLItemET>();
                locationList.Add(new DDLItemET { ITEM_TEXT = "38-HAVI", ITEM_VALUE = "38-HAVI" });
                locationList.Add(new DDLItemET { ITEM_TEXT = "38-WH", ITEM_VALUE = "38-WH" });
                vm.DSSearchCriteriaVM.locationList = new List<DDLItemET>();
                vm.DSSearchCriteriaVM.locationList = locationList;

                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        
        public DeliveryScheduleVM SearchDS(DeliveryScheduleVM vm)
        {
            try
            {
                DeliveryScheduleDC dc = new DeliveryScheduleDC();
                if (vm.DSSearchCriteriaVM == null) vm.DSSearchCriteriaVM = new DeliveryScheduleSearchCriteriaVM();
                if (vm.DSResultVM == null) vm.DSResultVM = new DeliveryScheduleResultVM();
                if (vm.DSResultVM.resultList == null) vm.DSResultVM.resultList = new List<DeliveryScheduleET>();

                vm.DSResultVM.resultList = dc.SearchDS(vm.DSSearchCriteriaVM.BRAND_CODE, vm.DSSearchCriteriaVM.BRANCH_CODE, vm.DSSearchCriteriaVM.LOCATION_CODE);
                InnitialCriteria(vm);
                return vm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int EditSave(DeliveryScheduleET data)
        {
            try
            {
                DeliveryScheduleDC dc = new DeliveryScheduleDC();
                return dc.EditSave(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int AddSave(DeliveryScheduleET data)
        {
            try
            {
                DeliveryScheduleDC dc = new DeliveryScheduleDC();
                return dc.AddSave(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
