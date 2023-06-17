using BO;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public static class Common
    {
        public static PO.Cart ConvertToPoCart(BO.Cart? Bc)
        {
            PO.OrderItem Poi = new PO.OrderItem();
            PO.Cart Pc = new()
            {
                CustomerAddress = Bc.CustomerAddress,
                CustomerEmail = Bc.CustomerEmail,
                CustomerName = Bc.CustomerName,
                TotalPrice = Bc.TotalPrice
            };
            foreach (BO.OrderItem? item in Bc.Items)
            {
                Poi = convertItemsToPOOI(item);
                Pc.Items.Add(Poi);
                // Pc.TotalPrice+=Poi.TotalPrice;
            }
            return Pc;
        }
        public static PO.OrderItem convertItemsToPOOI(BO.OrderItem? oil)
        {
            PO.OrderItem item2 = new()
            {
                ID = oil.ID,
                Amount = oil.Amount,
                Price = oil.Price,
                ProductID = oil.ProductID,
                Name = oil.Name,
                TotalPrice = oil.TotalPrice
            };
            return item2;
        }
        public static BO.Cart ConvertToBoCart(PO.Cart Bp)
        {
            BO.OrderItem Boi = new();
            BO.Cart BCart = new()
            {
                CustomerAddress = Bp.CustomerAddress,
                CustomerEmail = Bp.CustomerEmail,
                CustomerName = Bp.CustomerName,
                TotalPrice = Bp.TotalPrice,
            };
            foreach (PO.OrderItem? item in Bp.Items)
            {
                Boi = convertItemsToBOOI(item);
                BCart.Items.Add(Boi);
            }
            return BCart;
        }
        public static BO.OrderItem convertItemsToBOOI(PO.OrderItem oil)
        {
            BO.OrderItem item2 = new()
            {
                ID = oil.ID,
                Amount = oil.Amount,
                Price = oil.Price,
                ProductID = oil.ProductID,
                Name = oil.Name,
                TotalPrice = oil.TotalPrice
            };
            return item2;
        }
        public static PO.ProductForList ConvertToPoPFL(BO.ProductForList Bp)
        {
            PO.ProductForList item = new()
            {
                ID = Bp.ID,
                Name = Bp.Name,
                Price = Bp.Price,
                Category = (BO.Enums.eCategory)Bp.Category
            };
            return item;
        }
        public static PO.OrderForList ConvertToPoOFL(BO.OrderForList Bp)
        {
            PO.OrderForList item = new()
            {
                ID = Bp.ID,
                CustomerName = Bp.CustomerName,
                TotalPrice = Bp.TotalPrice,
                Status = (BO.Enums.eOrderStatus)Bp.Status,
                AmountOfItems = Bp.AmountOfItems
            };
            return item;
        }
        public static PO.ProductItem ConvertToPoPI(BO.ProductItem Bp)
        {
            PO.ProductItem item = new()
            {
                ID = Bp.ID,
                Name = Bp.Name,
                Price = Bp.Price,
                Category = (BO.Enums.eCategory)Bp.Category,
                Amount = Bp.Amount,
                InStock = Bp.InStock,
            };
            return item;
        }
    }
}