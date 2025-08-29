using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZEN.SaleAndTranfer.ET.AUT;
using ZEN.SaleAndTranfer.UI.DC2;
using ZEN.SaleAndTranfer.UI.ET2;
using ZEN.SaleAndTranfer.UI.Util;
using ZEN.SaleAndTranfer.UI.VM2.Prq;

namespace ZEN.SaleAndTranfer.UI.BC2
{
    public class PrqBC2
    {
        internal SearchItemVM2 SearchItem_OnInit(int? prCategoryID, string itemCode, UserLoginInfoET userInfo)
        {
            try
            {
                var vm = new SearchItemVM2();
                vm.Pet = new USP_R_ST_MAP_WH_ITEM__Search__Pet();
                vm.Pet.CurrentPageId = 1;
                vm.Pet.PageSize = PagingHelper.PageSize(PagingHelper.SearchItem);
                vm.Pet.StPrCateogryID = prCategoryID;
                vm.Pet.ItemCode = itemCode;

                var ddlDC = new DdlDC2();
                vm.CategoryDdl = ddlDC.GetPRCategoryBrand(brandCode: userInfo.BRAND_CODE);

                return vm;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        internal SearchItemVM2 SearchItem(SearchItemVM2 vm)
        {
            try
            {
                var dc = new PrqDC2();
                vm.SearchResult = dc.SearchItem(pet: vm.Pet);
                //if (vm.Pet.FormID == "#formSearch")
                //{
                //    vm.SearchCountAll = vm.SearchResult.Count;
                //}
                //else
                //{
                    vm.SearchCountAll = dc.SearchItemCountAll(pet: vm.Pet);
                //}
                
                return vm;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        internal PrqHET CreatePr(PrqHET et, List<BasketItemET> orders)
        {
            try
            {
                var dc = new PrqDC2();
                et = dc.Insert(het: et, orders: orders);
                return et;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        internal SearchVM2 Search_OnInit()
        {
            try
            {
                var vm = new SearchVM2();
                vm.Pet = new USP_R_ST_PR_Search_V2__Pet();
                vm.Pet.CurrentPageId = 1;
                vm.Pet.PageSize = PagingHelper.PageSize(PagingHelper.SearchPr);
                return vm;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        internal SearchVM2 Search(SearchVM2 vm)
        {
            try
            {
                var dc = new PrqDC2();
                vm.Result =  dc.Search(pet: vm.Pet);
                vm.SearchCountAll = dc.SearchCountAll(pet: vm.Pet);
                return vm;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        internal DetailVM Detail_OnInit(string prCode)
        {
            try
            {
                var vm = new DetailVM();
                var dc = new PrqDC2();
                vm.Het = dc.GetPrInfo(prCode: prCode);
                return vm;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        internal bool CanclePr(string prCode,string updateby)
        {
            try
            {

                var dc = new PrqDC2();
                return dc.cancle(prCode : prCode, updateby: updateby);
        
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



    }
}