using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZEN.SaleAndTranfer.UI.ET2;
using ZEN.SaleAndTranfer.UI.Models;

namespace ZEN.SaleAndTranfer.UI.DC2
{
    public class PrqDC2
    {
        public List<USP_R_ST_MAP_WH_ITEM__Search_Result> SearchItem(USP_R_ST_MAP_WH_ITEM__Search__Pet pet)
        {
            try
            {
                List<USP_R_ST_MAP_WH_ITEM__Search_Result> result = null;

                using (var db = new MainEntities())
                {
                    result = db.USP_R_ST_MAP_WH_ITEM__Search(
                        sT_PR_CATEGORY_ID: pet.StPrCateogryID,
                        iTEM_CODE: pet.ItemCode,
                        iTEM_NAME: pet.ItemName,
                        orderByList: pet.OrderByList,
                        pageSize: pet.PageSize,
                        currentPageId: pet.CurrentPageId,
                        username: pet.Username,
                        branchCode: pet.BranchCode,
                        brandCode: pet.BrancnCode
                        ).ToList();
                }

                //List<USP_R_ST_MAP_WH_ITEM__Search_Result> itemList = new List<USP_R_ST_MAP_WH_ITEM__Search_Result>();
                //USP_R_ST_MAP_WH_ITEM__Search_Result itemTmp = new USP_R_ST_MAP_WH_ITEM__Search_Result();
                //USP_R_ST_MAP_WH_ITEM__Search_Result itemOld = new USP_R_ST_MAP_WH_ITEM__Search_Result();
                //string oldItemCode = "";
                //bool start = true;
                //int i = 1;
                //foreach (var item in result)
                //{
                //    if (start)
                //    {

                //        oldItemCode = item.ItemCode;
                //        itemOld = item;
                //        itemTmp = new USP_R_ST_MAP_WH_ITEM__Search_Result();
                //        itemTmp.UOMList = item.RequestUomCode;
                //        itemTmp.PriceList = item.UnitPrice.ToString();
                //        if (result.Count == 1)
                //        {
                //            itemTmp.DeliveryUomCode = itemOld.DeliveryUomCode;
                //            itemTmp.ItemCode = itemOld.ItemCode;
                //            itemTmp.ItemName = itemOld.ItemName;
                //            itemTmp.OrderQty = itemOld.OrderQty;
                //            itemTmp.RequestByBranchCode = itemOld.RequestByBranchCode;
                //            itemTmp.RequestByBrandCode = itemOld.RequestByBrandCode;
                //            itemTmp.RequestToBrandCode = itemOld.RequestToBrandCode;
                //            itemTmp.RequestToLocationCode = itemOld.RequestToLocationCode;
                //            itemTmp.RequestUomCode = itemOld.RequestUomCode;
                //            itemTmp.UnitPrice = itemOld.UnitPrice;
                //            itemTmp.RowNumber = itemOld.RowNumber;
                //            itemTmp.StMapWhItemID = itemOld.StMapWhItemID;
                //            itemTmp.StWhitemCategoryID = itemOld.StWhitemCategoryID;

                //            oldItemCode = item.ItemCode;
                //            itemOld = new USP_R_ST_MAP_WH_ITEM__Search_Result();
                //            itemOld = item;
                //            itemList.Add(itemTmp);
                //        }
                //        else
                //        {
                //            start = false;
                //        }
                //    }
                //    else if (item.ItemCode == oldItemCode)
                //    {
                //        itemTmp.UOMList += "|" + item.RequestUomCode;
                //        itemTmp.PriceList += "|" + item.UnitPrice.ToString();
                //        if (i == result.Count)
                //        {
                //            itemTmp.DeliveryUomCode = itemOld.DeliveryUomCode;
                //            itemTmp.ItemCode = itemOld.ItemCode;
                //            itemTmp.ItemName = itemOld.ItemName;
                //            itemTmp.OrderQty = itemOld.OrderQty;
                //            itemTmp.RequestByBranchCode = itemOld.RequestByBranchCode;
                //            itemTmp.RequestByBrandCode = itemOld.RequestByBrandCode;
                //            itemTmp.RequestToBrandCode = itemOld.RequestToBrandCode;
                //            itemTmp.RequestToLocationCode = itemOld.RequestToLocationCode;
                //            itemTmp.RequestUomCode = itemOld.RequestUomCode;
                //            itemTmp.UnitPrice = itemOld.UnitPrice;
                //            itemTmp.RowNumber = itemOld.RowNumber;
                //            itemTmp.StMapWhItemID = itemOld.StMapWhItemID;
                //            itemTmp.StWhitemCategoryID = itemOld.StWhitemCategoryID;

                //            oldItemCode = item.ItemCode;
                //            itemOld = new USP_R_ST_MAP_WH_ITEM__Search_Result();
                //            itemOld = item;
                //            itemList.Add(itemTmp);
                //        }
                //    }
                //    else
                //    {
                //        itemTmp.DeliveryUomCode = itemOld.DeliveryUomCode;
                //        itemTmp.ItemCode = itemOld.ItemCode;
                //        itemTmp.ItemName = itemOld.ItemName;
                //        itemTmp.OrderQty = itemOld.OrderQty;
                //        itemTmp.RequestByBranchCode = itemOld.RequestByBranchCode;
                //        itemTmp.RequestByBrandCode = itemOld.RequestByBrandCode;
                //        itemTmp.RequestToBrandCode = itemOld.RequestToBrandCode;
                //        itemTmp.RequestToLocationCode = itemOld.RequestToLocationCode;
                //        itemTmp.RequestUomCode = itemOld.RequestUomCode;
                //        itemTmp.UnitPrice = itemOld.UnitPrice;
                //        itemTmp.RowNumber = itemOld.RowNumber;
                //        itemTmp.StMapWhItemID = itemOld.StMapWhItemID;
                //        itemTmp.StWhitemCategoryID = itemOld.StWhitemCategoryID;
                      
                //        oldItemCode = item.ItemCode;
                //        itemOld = new USP_R_ST_MAP_WH_ITEM__Search_Result();
                //        itemOld = item;
                //        itemList.Add(itemTmp);
                //        itemTmp = new USP_R_ST_MAP_WH_ITEM__Search_Result();
                //        itemTmp.UOMList = item.RequestUomCode;
                //        itemTmp.PriceList = item.UnitPrice.ToString();

                //    }
                //    i++;
                //}

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int SearchItemCountAll(USP_R_ST_MAP_WH_ITEM__Search__Pet pet)
        {
            try
            {
                int? result = 0;

                using (var db = new MainEntities())
                {
                    result = db.USP_R_ST_MAP_WH_ITEM__SearchCountAll(
                        sT_PR_CATEGORY_ID: pet.StPrCateogryID,
                        iTEM_CODE: pet.ItemCode,
                        iTEM_NAME: pet.ItemName,
                        orderByList: pet.OrderByList,
                        pageSize: pet.PageSize,
                        currentPageId: pet.CurrentPageId,
                        username: pet.Username).FirstOrDefault();
                }

                return result != null && result.HasValue ? result.Value : 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        internal PrqHET Insert(PrqHET het, List<BasketItemET> orders)
        {
            try
            {
                using (var db = new MainEntities())
                {
                    using (var trx = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var prCode = string.Empty;

                            prCode = db.USP_R_ST_PR_H_Insert_V2(
                                bRANCH_CODE: het.BranchCode,
                                 pLAN_DELIVERY_DATE: het.PlanDeliveryDate,
                                 cREATE_BY: het.CreateBy,
                                 mOBILE_NO: het.MobileNo,
                                 cUS_PO_CODE: het.PoCode,
                                 pR_REMARK: het.Remark,
                                 pR_BY: het.OrderBy,
                                 aDDR: het.Address,
                                 aDDR2: het.Address2,
                                 aDDR3: het.Address3,
                                 pOSTCODE: het.Postcode,
                                 cITY: het.City,
                                 iS_SALE_ADMIN: het.isSaleAdmin,
                                 sALE_ADMIN_CODE: het.SaleAdminCode
                                ).FirstOrDefault();

                            db.SaveChanges();

                            if (string.IsNullOrWhiteSpace(prCode))
                            {
                                throw new Exception("Insert Pr Header Error | Pr Code is null or empty.");
                            }

                            het.PrCode = prCode;

                            if (orders != null && orders.Count > 0)
                            {
                                foreach (var order in orders)
                                {
                                    db.USP_R_ST_PR_D_Insert_V2(stMapWhItemID: order.StMapWhItemID, orderQty: order.OrderQty, itemCode: order.ItemCode, requestUomCode: order.RequestUomCode, itemName: order.ItemName, unitPrice: order.UnitPrice, remark: order.Remark, createBy: het.CreateBy, prCode: prCode, brandCode: het.BrandCode, branchCode: het.BranchCode);
                                }

                                //db.USP_R_ST_PR_SendToNavision(p_ST_PR_CODE: prCode);
                            }
                            else
                            {
                                throw new Exception("Error Create PR : No have detail.");
                            }


                            db.SaveChanges();
                            trx.Commit();
                        }
                        catch (Exception tex)
                        {
                            trx.Rollback();
                            throw tex;
                        }
                    }
                }

                return het;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        internal bool cancle(string prCode, string updateby)
        {
            try
            {
                using (var db = new MainEntities())
                {
                    using (var trx = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var prCode1 = string.Empty;
                            db.USP_R_ST_PR_H_Update_FLAG(p_ST_PR_CODE: prCode, p_DELETE_FLAG: true, p_UPDATE_BY: updateby);
                            db.SaveChanges();
                            trx.Commit();
                        }
                        catch (Exception tex)
                        {
                            trx.Rollback();
                            throw tex;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<USP_R_ST_PR_Search_V2_Result> Search(USP_R_ST_PR_Search_V2__Pet pet)
        {
            try
            {
                List<USP_R_ST_PR_Search_V2_Result> result = null;
                if (pet is null)
                {
                    return result;
                }
                using (var db = new MainEntities())
                {
                    result = db.USP_R_ST_PR_Search_V2(prCode: pet.PrCode, planDeliveryDateFrom: pet.PlanDeliveryDateFrom, planDeliveryDateTo: pet.PlanDeliveryDateTo, createDateFrom: pet.CreateDateFrom, createDateTo: pet.CreateDateTo, customerPoCode: pet.CustomerPoCode, username: pet.Username, orderByList: pet.OrderByList, pageSize: pet.PageSize, currentPageId: pet.CurrentPageId, iS_SALE_ADMIN: pet.isSaleAdmin, sALE_ADMIN_CODE: pet.saleAdminCode, nAME_2 : pet.Name_2).ToList();
                }

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int SearchCountAll(USP_R_ST_PR_Search_V2__Pet pet)
        {
            try
            {
                int? result = 0;

                using (var db = new MainEntities())
                {
                    result = db.USP_R_ST_PR_SearchCountAll_V2(prCode: pet.PrCode, planDeliveryDateFrom: pet.PlanDeliveryDateFrom, planDeliveryDateTo: pet.PlanDeliveryDateTo, createDateFrom: pet.CreateDateFrom, createDateTo: pet.CreateDateTo, customerPoCode: pet.CustomerPoCode, username: pet.Username, orderByList: pet.OrderByList, pageSize: pet.PageSize, currentPageId: pet.CurrentPageId, iS_SALE_ADMIN: pet.isSaleAdmin, sALE_ADMIN_CODE: pet.saleAdminCode, nAME_2: pet.Name_2).FirstOrDefault();
                }

                return result != null && result.HasValue ? result.Value : 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public USP_R_ST_PR_H__GetByPK_Result GetPrq(string prCode)
        {
            try
            {
                USP_R_ST_PR_H__GetByPK_Result result = null;

                using (var db = new MainEntities())
                {
                    result = db.USP_R_ST_PR_H__GetByPK(prCode: prCode).FirstOrDefault();
                }

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PrqHET GetPrInfo(string prCode)
        {
            try
            {
                PrqHET result = null;
                USP_R_ST_PR_H__GetByPK_Result header = null;

                using (var db = new MainEntities())
                {
                    header = db.USP_R_ST_PR_H__GetByPK(prCode: prCode).FirstOrDefault();
                }

                if (header == null)
                {
                    return null;
                }
                else
                {
                    result = new PrqHET();
                    result.BranchCode = null;
                    result.CreateBy = header.CREATE_BY;
                    result.MobileNo = header.MOBILE_NO;
                    result.OrderBy = header.PR_BY;
                    result.OrderDate = header.CREATE_DATE;
                    result.PlanDeliveryDate = header.PLAN_DELIVERY_DATE;
                    result.PoCode = header.CUS_PO_CODE;
                    result.PrCode = header.ST_PR_CODE;
                    result.Remark = header.PR_REMARK;
                    result.StPrCategoryID = null;
                    result.SumTotalPrice = header.SumSalePrice;
                }

                List<BasketItemET> detail = null;
                using (var db = new MainEntities())
                {
                    detail = (from det in db.USP_R_ST_PR_D__GetByPrCode(prCode: prCode)
                              select new BasketItemET()
                              {
                                  ItemCode = det.ITEM_CODE,
                                  ItemName = det.ITEM_FULL_NAME_TH,
                                  OrderQty = det.QTY,
                                  Remark = det.ITEM_REMARK,
                                  RequestUomCode = det.REQUEST_UOM_CODE,
                                  StMapWhItemID = det.ST_MAP_WH_ITEM_ID != null && det.ST_MAP_WH_ITEM_ID.HasValue ? det.ST_MAP_WH_ITEM_ID.Value : 0,
                                  UnitPrice = det.SALE_PRICE
                              }).ToList();
                }

                result.Detail = detail;

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}