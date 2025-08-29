using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.DC
{
    internal sealed class StoreProcConst
    {
        //Batch 
        public const string USP_R_BATCH_PROCESS_Start = "USP_R_BATCH_PROCESS_Start";
        public const string USP_R_BATCH_PROCESS_Error = "USP_R_BATCH_PROCESS_Error";
        public const string USP_R_BATCH_PROCESS_End = "USP_R_BATCH_PROCESS_End";
        public const string USP_L_BATCH_PROCESS__Insert = "USP_L_BATCH_PROCESS__Insert";
        public const string Usp_R_ExportSaesTranTOERP = "Usp_R_ExportSaesTranTOERP";
        
        
        //Config
        public const string USP_C_CON_GetConfigByPK = "USP_C_CON_GetConfigByPK";

        //MESSAGE
        public const string USP_M_GetMessage = "USP_M_GetMessage";

        //LOG
        public const string USP_L_InsertExceptionLog = "USP_L_InsertExceptionLog";

        //AUT
        public const string USP_MAS_UserSearch = "USP_MAS_UserSearch";
        public const string USP_ST_MAS_UserSearch = "USP_ST_MAS_UserSearch";
        public const string USP_ST_MAS_UserSearchByID = "USP_ST_MAS_UserSearchByID";
        public const string USP_AUR_GetMenuPermissionByUsername = "USP_AUR_GetMenuPermissionByUsername";
        public const string USP_M_USER_IsExistUsername = "USP_M_USER_IsExistUsername";

        //User
        public const string USP_USM_UpdateUser = "USP_USM_UpdateUser";
        public const string USP_ST_USM_InsertUser = "USP_ST_USM_InsertUser";

        //DDL
        public const string USP_DDL_GetStatus = "USP_DDL_GetStatus";
        public const string USP_DDL_GetBrand = "USP_DDL_GetBrand";
        public const string USP_DDL_GetBranchbyBrand = "USP_DDL_GetBranchbyBrand";
        public const string USP_DDL_GetBranchbyBrandAndUsername = "USP_DDL_GetBranchbyBrandAndUsername";
        public const string USP_DDL_GetBrandByUsername = "USP_DDL_GetBrandByUsername";
        public const string USP_DDL_GetBranchByUsername = "USP_DDL_GetBranchByUsername";
        public const string USP_DDL_ST_DO_H_GetByPRCode = "USP_DDL_ST_DO_H_GetByPRCode";
        public const string USP_DDL_GetSTCategoryWh = "USP_DDL_GetSTCategoryWh";
        public const string USP_DDL_GetSTLocation = "USP_DDL_GetSTLocation";
        public const string USP_DDL_GetCompany = "USP_DDL_GetCompany";
        public const string USP_DDL_GetAppSystem = "USP_DDL_GetAppSystem";
        public const string USP_DDL_GetItemStockType = "USP_DDL_GetItemStockType";

        //CheckBox
        public const string USP_CBX_GetBranchbyBrandAndUsername = "USP_CBX_GetBranchbyBrandAndUsername";
        public const string USP_CBX_GetBrandByUsername = "USP_CBX_GetBrandByUsername";
        
        // Dashboard
        public const string USP_R_ST_PR_GetDashboard = "USP_R_ST_PR_GetDashboard";
        public const string USP_R_ST_DO_GetDashboard = "USP_R_ST_DO_GetDashboard";

        //PR
        public const string USP_R_ST_PR_Search = "USP_R_ST_PR_Search";
        public const string USP_R_ST_SearchShoppingItem = "USP_R_ST_SearchShoppingItem";
        public const string USP_R_ST_MAP_PR_CATEGORY_BRAND_GetByBrandCode = "USP_R_ST_MAP_PR_CATEGORY_BRAND_GetByBrandCode";
        public const string USP_R_ST_PR_D_Insert = "USP_R_ST_PR_D_Insert";
        public const string USP_R_ST_PR_H_Insert = "USP_R_ST_PR_H_Insert";
        public const string USP_R_ST_PR_D_Update = "USP_R_ST_PR_D_Update";
        public const string USP_R_ST_PR_D_Delete = "USP_R_ST_PR_D_Delete";
        public const string USP_R_ST_PR_H_Update = "USP_R_ST_PR_H_Update";
        public const string USP_R_ST_PR_H_GetByPRCode = "USP_R_ST_PR_H_GetByPRCode";
        public const string USP_R_ST_PR_D_GetByPRCode = "USP_R_ST_PR_D_GetByPRCode";
        public const string USP_R_ST_PR_SendToNavision = "USP_R_ST_PR_SendToNavision";
        public const string USP_R_ST_PR_H_Delete = "USP_R_ST_PR_H_Delete";
        public const string USP_R_ST_PR_CAL_AVG_ITEM = "USP_R_ST_PR_CAL_AVG_ITEM";
        public const string USP_R_ST_CheckPRDupplicate = "USP_R_ST_CheckPRDupplicate";
        
        //UFN
        public const string UFN_GEN_ST_PR_NO = "UFN_GEN_ST_PR_NO";
        
        //DO
        public const string USP_R_ST_DO_SEARCH = "USP_R_ST_DO_SEARCH";
        public const string USP_R_ST_DO_H_GetByDOCode = "USP_R_ST_DO_H_GetByDOCode";
        public const string USP_R_ST_DO_D_GetByDOCode = "USP_R_ST_DO_D_GetByDOCode";

        //GR
        public const string USP_R_ST_GR_SEARCH = "USP_R_ST_GR_SEARCH";
        public const string USP_R_ST_GR_H_GetByGRCode = "USP_R_ST_GR_H_GetByGRCode";
        public const string USP_R_ST_GR_D_GetByGRCode = "USP_R_ST_GR_D_GetByGRCode";
        public const string USP_R_ST_GR_H_Insert = "USP_R_ST_GR_H_Insert";
        public const string USP_R_ST_GR_D_Insert = "USP_R_ST_GR_D_Insert";
        public const string USP_R_ST_GR_H_Update = "USP_R_ST_GR_H_Update";
        public const string USP_R_ST_GR_D_Update = "USP_R_ST_GR_D_Update";
        public const string USP_R_ST_GR_Delete = "USP_R_ST_GR_Delete";

        //Batch
        public const string USP_BATCH_ST_PR_Run = "USP_BATCH_ST_PR_Run";
        public const string USP_BATCH_ST_DO_Run = "USP_BATCH_ST_DO_Run";
        public const string USP_ST_CALL_JOB_SEND_PR = "USP_ST_CALL_JOB_SEND_PR";
        public const string USP_ST_CALL_JOB_GET_DO = "USP_ST_CALL_JOB_GET_DO";
        public const string USP_BATCH_ST_GR_Run = "USP_BATCH_ST_GR_Run";
        public const string USP_R_BATCH_MASTER = "USP_R_BATCH_MASTER";
        public const string USP_R_BATCH_ST_PR_Sale = "USP_R_BATCH_ST_PR_Sale";
        public const string USP_R_BATCH_ST_GR_Sale = "USP_R_BATCH_ST_GR_Sale";
        public const string USP_T_ST_InsertPRSendNavision = "USP_T_ST_InsertPRSendNavision";

        //Batch HAVI
        public const string USP_HAVI_CALL_JOB_RN = "USP_HAVI_CALL_JOB_RN";
        public const string USP_HAVI_CALL_JOB_PO = "USP_HAVI_CALL_JOB_PO";
        public const string USP_HAVI_CALL_JOB_SO = "USP_HAVI_CALL_JOB_SO";
        public const string USP_HAVI_CALL_JOB_DN = "USP_HAVI_CALL_JOB_DN";

        //Export
        public const string USP_ST_Download_LOCATION = "USP_ST_Download_LOCATION";
        public const string USP_ST_Download_WH_ITEM_CATEGORY = "USP_ST_Download_WH_ITEM_CATEGORY";
        public const string USP_ST_Download_UNIT = "USP_ST_Download_UNIT";
        public const string USP_ST_Download_UNIT_CONVERT = "USP_ST_Download_UNIT_CONVERT";
        public const string USP_ST_Download_WH_MAP_ITEM = "USP_ST_Download_WH_MAP_ITEM";
        public const string USP_ST_Download_BRAND_CATEGORY = "USP_ST_Download_BRAND_CATEGORY";
        public const string USP_ST_Download_BRAND_MAP_ITEM_CATEGORY = "USP_ST_Download_BRAND_MAP_ITEM_CATEGORY";
        public const string USP_ST_Download_BRAND_MAP_BRAND_CATEGORY = "USP_ST_Download_BRAND_MAP_BRAND_CATEGORY";
        public const string USP_ST_Download_PR_WH_Pickup = "USP_ST_Download_PR_WH_Pickup";
        
        //Import
        public const string USP_ST_validate_Upload_WH_ITEM_CATEGORY = "USP_ST_validate_Upload_WH_ITEM_CATEGORY";
        public const string USP_ST_Upload_WH_ITEM_CATEGORY = "USP_ST_Upload_WH_ITEM_CATEGORY";
        public const string USP_ST_validate_Upload_LOCATION = "USP_ST_validate_Upload_LOCATION";
        public const string USP_ST_Upload_LOCATION = "USP_ST_Upload_LOCATION";
        public const string USP_ST_Validate_Upload_MAP_WH_ITEM = "USP_ST_Validate_Upload_MAP_WH_ITEM";
        public const string USP_ST_Upload_MAP_WH_ITEM = "USP_ST_Upload_MAP_WH_ITEM";
        public const string USP_ST_validate_Upload_NON_NAV_UNIT = "USP_ST_validate_Upload_NON_NAV_UNIT";
        public const string USP_ST_Upload_NON_NAV_UNIT = "USP_ST_Upload_NON_NAV_UNIT";
        public const string USP_ST_validate_Upload_UNIT_CONVERT = "USP_ST_validate_Upload_UNIT_CONVERT";
        public const string USP_ST_Upload_UNIT_CONVERT = "USP_ST_Upload_UNIT_CONVERT";
        public const string USP_ST_validate_Upload_PR_CATEGORY = "USP_ST_validate_Upload_PR_CATEGORY";
        public const string USP_ST_Upload_PR_CATEGORY = "USP_ST_Upload_PR_CATEGORY";
        public const string USP_ST_validate_Upload_MAP_PR_CATEGORY_ITEM = "USP_ST_validate_Upload_MAP_PR_CATEGORY_ITEM";
        public const string USP_ST_Upload_MAP_PR_CATEGORY_ITEM = "USP_ST_Upload_MAP_PR_CATEGORY_ITEM";
        public const string USP_ST_validate_Upload_MAP_PR_CATEGORY_BRAND = "USP_ST_validate_Upload_MAP_PR_CATEGORY_BRAND";
        public const string USP_ST_Upload_MAP_PR_CATEGORY_BRAND = "USP_ST_Upload_MAP_PR_CATEGORY_BRAND";
        
        //Download
        public const string USP_R_DOWNLOAD_SearchFiles = "USP_R_DOWNLOAD_SearchFiles";         
       
        //Stock
        public const string USP_R_STOCK_CARD_GetItem = "USP_R_STOCK_CARD_GetItem";
        public const string USP_R_STOCK_CARD_SaveTransaction = "USP_R_STOCK_CARD_SaveTransaction";
        public const string USP_R_STOCK_CARD_Search = "USP_R_STOCK_CARD_Search";
        public const string USP_M_STOCK_UNIT_CONVERT_Search = "USP_M_STOCK_UNIT_CONVERT_Search";
        public const string USP_M_STOCK_UNIT_CONVERT_Upload = "USP_M_STOCK_UNIT_CONVERT_Upload";
        public const string USP_M_STOCK_UNIT_CONVERT_validate_Upload = "USP_M_STOCK_UNIT_CONVERT_validate_Upload";

        //Report
        public const string USP_RPT_ST_REPORT_WHO_REC_ITEM = "USP_RPT_ST_REPORT_WHO_REC_ITEM";
        public const string USP_RPT_ST_REPORT_SEND_AND_RECEIVE = "USP_RPT_ST_REPORT_SEND_AND_RECEIVE";

        //Signature
        public const string USP_M_ST_SIGNATURE_Search = "USP_M_ST_SIGNATURE_Search";
        public const string USP_M_ST_SIGNATURE_Update = "USP_M_ST_SIGNATURE_Update";
        public const string USP_M_ST_SIGNATURE_Insert = "USP_M_ST_SIGNATURE_Insert";

        //Receive Transfer
        public const string USP_R_ST_RECEIVE_TRANSFER = "USP_R_ST_RECEIVE_TRANSFER";
        public const string USP_RPT_ST_RECEIVE_TRANSFER = "USP_RPT_ST_RECEIVE_TRANSFER";

        //End Day Report for ACC  Supaneej, 2018-11-08,
        public const string USP_DDL_GetEndDayDocType = "USP_DDL_GetEndDayDocType";
        public const string USP_R_END_DAY_DOC_Insert = "USP_R_END_DAY_DOC_Insert";
        public const string USP_R_END_DAY_DOC_GetByEndDayDate = "USP_R_END_DAY_DOC_GetByEndDayDate";
        public const string USP_R_END_DAY_DOC_Delete = "USP_R_END_DAY_DOC_Delete";
        public const string USP_R_END_DAY_DOC_UnDelete = "USP_R_END_DAY_DOC_UnDelete";
        public const string USP_R_END_DAY_DOC_GetByPK = "USP_R_END_DAY_DOC_GetByPK";
        public const string USP_M_END_DAY_DOC_TYPE__GetByPK = "USP_M_END_DAY_DOC_TYPE__GetByPK";

        public const string USP_R_ST_GR_GET_COMPLAIN = "USP_R_ST_GR_GET_COMPLAIN";  //Supaneej, 2018-10-12, COMPLAIN     
        public const string USP_M_COMPLAIN_GetByType = "USP_M_COMPLAIN_GetByType";
        public const string USP_R_RESULT_COMPLAIN_GetByGrCode = "USP_R_RESULT_COMPLAIN_GetByGrCode";
        public const string USP_R_ST_GR_COMPLAIN_Save = "USP_R_ST_GR_COMPLAIN_Save";


        // 1.) For Franchises-> Pricing, Image, Complaint
        // 2.) For Equity-> Stock
        public const string USP_M_BRANCH__IsFranchises = "USP_M_BRANCH__IsFranchises";


        public const string USP_R_ST_PR_GetAlertAvgPRQty = "USP_R_ST_PR_GetAlertAvgPRQty";

        //Location EQ
        public const string USP_DDL_GetFCLocation = "USP_DDL_GetFCLocation";

        //PR_BACK_TO_NEW by K.Dang Warehouse
        public const string USP_R_ST_PR_BACK_TO_NEW = "USP_R_ST_PR_BACK_TO_NEW";

        //Delivery Schedule
        public const string USP_M_ST_DELIVERY_SCHEDULE__GetData = "USP_M_ST_DELIVERY_SCHEDULE__GetData";
        public const string USP_M_ST_DELIVERY_SCHEDULE__Update = "USP_M_ST_DELIVERY_SCHEDULE__Update";
        public const string USP_M_ST_DELIVERY_SCHEDULE__Insert = "USP_M_ST_DELIVERY_SCHEDULE__Insert";

        //Block User FC
        public const string USP_MAS_USER_UpdateActiveFlag = "USP_MAS_USER_UpdateActiveFlag";

        //GR Patial
        public const string USP_DDL_GetDOStatusForGR = "USP_DDL_GetDOStatusForGR";

        //Stock card by P'Fern
        public const string USP_R_STOCK_CARD_CheckAlreadyCount = "USP_R_STOCK_CARD_CheckAlreadyCount";
        
        
        

    }
}
