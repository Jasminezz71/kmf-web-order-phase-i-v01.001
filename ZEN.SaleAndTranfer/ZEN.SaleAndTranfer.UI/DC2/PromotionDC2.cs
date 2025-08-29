using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZEN.SaleAndTranfer.UI.ET2;
using ZEN.SaleAndTranfer.UI.Models;

namespace ZEN.SaleAndTranfer.UI.DC2
{
    public class PromotionDC2
    {
        public List<PopupPromotionItemRet> GetPopupPromotionItem(string username)
        {
            try
            {
                List<PopupPromotionItemRet> result = null;

                using (var db = new MainEntities())
                {
                    result = (from ret in db.USP_R_ST_MAP_WH_ITEM__GetPopupPromoItem(username: username)
                              select new PopupPromotionItemRet()
                              {
                                  ItemCode = ret
                              }).ToList();
                }

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        internal USP_C_CONFIG__SavePopupPromotionItem__Pet GetPopupPromotionItemConfig()
        {
            try
            {
                USP_C_CONFIG__SavePopupPromotionItem__Pet result = null;

                using (var db = new MainEntities())
                {
                    result = (from ret in db.USP_C_CONFIG__GetPopupPromoItem()
                              select new USP_C_CONFIG__SavePopupPromotionItem__Pet()
                              {
                                  ItemCodes = ret.CONFIG_VALUE
                                  ,
                                  UpdateBy = ret.UPDATE_BY
                              }
                              ).FirstOrDefault();
                }

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        internal bool? HasPopupPromotionItemConfig()
        {
            try
            {
                bool? result = null;

                using (var db = new MainEntities())
                {
                    result = db.USP_C_CONFIG__HasPopupPromoItem().FirstOrDefault();
                }

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        internal void SavePopupPromotionItemConfig(USP_C_CONFIG__SavePopupPromotionItem__Pet pet)
        {
            try
            {
                using (var db = new MainEntities())
                {
                    using (var trx = db.Database.BeginTransaction())
                    {
                        try
                        {
                            db.USP_C_CONFIG__SavePopupPromotionItem(itemCodes: pet.ItemCodes, updateBy: pet.UpdateBy);
                            db.SaveChanges();
                            trx.Commit();
                        }
                        catch (Exception eex)
                        {
                            trx.Rollback();
                            throw eex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}