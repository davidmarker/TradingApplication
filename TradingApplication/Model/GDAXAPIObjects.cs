using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TradingApplication.API
{
    public class Products
    {
        public List<Product> product_list { get; set; }
    }

    public class Product
    {
        public string id { get; set; }
        public string base_currency { get; set; }
        public string quote_currency { get; set; }
        public string base_min_size { get; set; }
        public string base_max_size { get; set; }
        public string quote_increment { get; set; }
        public string display_name { get; set; }
    }

    public class Accounts
    {
        public List<Account> account_list { get; set; }
    }
    public class Account
    {
        public string id { get; set; }
        public string currency { get; set; }
        public string balance { get; set; }
        public string available { get; set; }
        public string hold { get; set; }
        public string profile_id { get; set; }
    }

    public class Ledger
    {
        public List<AccountHistory> account_hist_list { get; set; }
    }

    public class AccountHistory
    {
        public string id { get; set; }
        public string created_at { get; set; }
        public string amount { get; set; }
        public string balance { get; set; }
        public string type { get; set; }
        public Details details { get; set; }
    }

    public class Details
    {
        public string order_id { get; set; }
        public string trade_id { get; set; }
        public string product_id { get; set; }
    }

    public class Holds
    {
        public List<Hold> hold_list { get; set; }
    }

    public class Hold
    {
        public string id { get; set; }
        public string account_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string amount { get; set; }
        public string type { get; set; }
        public string ref_id { get; set; }
    }

    public class ProdTicker
    {
        public int trade_id { get; set; }
        public string price { get; set; }
        public string size { get; set; }
        public string bid { get; set; }
        public string ask { get; set; }
        public string volume { get; set; }
        public string time { get; set; }
    }
    
    public class ProdStats
    {
        public string open { get; set; }
        public string high { get; set; }
        public string low { get; set; }
        public string volume { get; set; }
    }


}