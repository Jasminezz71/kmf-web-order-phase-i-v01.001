using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZEN.SaleAndTranfer.UI.DC2;
using ZEN.SaleAndTranfer.UI.ET2;
using ZEN.SaleAndTranfer.UI.VM2.MaPromotion;

namespace ZEN.SaleAndTranfer.UI.BC2
{
    public class PromotionBC2
    {
        internal List<PopupPromotionItemRet> GetPopupPromotionItem(string username)
        {
            try
            {
                var dc = new PromotionDC2();
                return dc.GetPopupPromotionItem(username: username);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        internal DoVM Do_OnInit()
        {
            try
            {
                var vm = new DoVM();
                var dc = new PromotionDC2();
                vm.Pet = dc.GetPopupPromotionItemConfig();
                return vm;
            }
            catch (Exception ex)
            {

                throw ex; 
            }
        }

        internal void SavePopupPromotionItem(USP_C_CONFIG__SavePopupPromotionItem__Pet pet)
        {
            try
            {
                var dc = new PromotionDC2();
                dc.SavePopupPromotionItemConfig(pet: pet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        internal bool? HasPopupPromotionItem()
        {
            try
            {
                var dc = new PromotionDC2();
                return dc.HasPopupPromotionItemConfig();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}