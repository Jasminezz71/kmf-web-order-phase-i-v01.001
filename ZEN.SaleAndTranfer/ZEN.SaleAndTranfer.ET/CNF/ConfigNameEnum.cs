using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZEN.SaleAndTranfer.ET.CNF
{
    public enum ConfigNameEnum
    {
        #region -- paging, pagination --
        PAGINATION,
        ROW_PER_PAGE,
        #endregion

        #region -- Email --
        ENABLESSL,
        HOST,
        NETWORK_PASSWORD,
        NETWORK_SUBJECT,
        NETWORK_DETAIL,
        NETWORK_USERNAME,
        PORT,
        #endregion

        DEFUALT_PASSWORD,
        CURRENT,
        VERSIONING,
        DOWNLOAD,

        MAX_SIZE,
        TYPE,

        #region -- report --
        DOMAIN_NAME,
        PASSWORD,
        URL,
        USER,
        REPORT_PATH, 
        RPT_ST_DO,
        RPT_ST_GR,
        RPT_ST_PR,
        RPT_ST_PR_FC,
        RPT_ST_PR_EQ,
        RPT_SALES_INVOICE,
        RPT_CREDIT_NOTE,
        RPT_STOCKCARD_SEARCH,
        RPT_STOCKCARD_GETITEM
        #endregion
            
        , WH_CATEGORY
        , LOCATION
        , WH_MAP_ITEM
        , BRAND_CATEGORY
        , BRAND_MAP_ITEM_CATEGORY
        , BRAND_MAP_BRAND_CATEGORY
        , UNIT
        , UNITCONVERSION
        , WH_PICKUP
        , PR_CATEGORY
        , UNITCONVERT

        , TIMEOUT
        , TIMEOUT_ALERT
        , TIMEOUT_MESSAGE
        , WAITING_TIME
        , STOCK_CARD

        , FORMAT_NAME
        , CONTENT_TYPE
        , FILENAME_REC_TRANSFER

        , ACC_END_DAY_DOC_ROOT_FOLDER

        , END_DAY_DOC_SIZE
        , END_DAY_DOC_TYPE
        , END_DAY_DOC_TYPE_COMPRESS_SCALE
        , UPLOAD_TEMP__ACC_END_DAY_DOC__USERNAME
        , UPLOAD_TEMP__ACC_END_DAY_DOC__PASSWORD
        , UPLOAD_TEMP__ACC_END_DAY_DOC__DOMAIN

        , SHOW_IMAGE_FLAG

    }
}
