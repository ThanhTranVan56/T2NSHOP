using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T2NSHOP.Models;
using T2NSHOP.Models.EF;
using T2NSHOP.Models.Payments;

namespace T2NSHOP.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = db.shoppingCarts.Where(x => x.IdUser == User.Identity.Name && x.IsDelete == false).ToList();
            var tongtien = decimal.Zero;
            if (cart != null && cart.Any())
            {

                foreach (var item in cart)
                {
                    tongtien += item.TotalPrice;
                }
                ViewBag.CheckCart = cart;
                ViewBag.Count = cart.Count();
                ViewBag.TotalPrice = tongtien;
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddtoCart(ShoppingCart model, int id, int quantity, string color, string size)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0, Idpro = 0, Size = "" };
            if (ModelState.IsValid)
            {
                var cart = db.shoppingCarts.Where(x => x.IdUser == User.Identity.Name && x.IsDelete == false).ToList();
                var checkExits = cart.SingleOrDefault(x => x.ProductId == id && x.Color == color && x.Size == size);
                if (checkExits != null)
                {
                    checkExits.Quantity += 1;
                    checkExits.TotalPrice = checkExits.Quantity * checkExits.Price;
                    db.shoppingCarts.Attach(checkExits);
                    db.Entry(checkExits).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    code = new { Success = true, msg = "Thêm sản phẩm vào giỏ hàng thành công", code = 1, Count = cart.Count, Idpro = id, Size = size };
                }
                else
                {

                    var checkProduct = db.Products.FirstOrDefault(x => x.Id == id);
                    if (checkProduct != null)
                    {
                        model.IdUser = User.Identity.Name;
                        model.ProductId = checkProduct.Id;
                        model.ProductName = checkProduct.Title;
                        model.CategoryName = checkProduct.ProductCategory.Titel;
                        model.Alias = checkProduct.Alias;
                        model.Quantity = quantity;
                        model.ProductImg = db.ProductImages.FirstOrDefault(x => x.ProductId == id && x.Color == color).Image;
                        model.Price = checkProduct.Price;
                        if (checkProduct.PriceSale > 0)
                        {
                            model.Price = (decimal)checkProduct.PriceSale;
                        }
                        model.TotalPrice = model.Quantity * model.Price;
                        model.Color = color;
                        model.Size = size;
                        model.IsDelete = false;
                        db.shoppingCarts.Add(model);
                        db.SaveChanges();
                        var cartadd = db.shoppingCarts.Where(x => x.IdUser == User.Identity.Name && x.IsDelete == false).ToList();
                        code = new { Success = true, msg = "Thêm sản phẩm vào giỏ hàng thành công", code = 1, Count = cartadd.Count, Idpro = id, Size = size };
                    }
                }
                return Json(code);
            }
            return Json(code);
        }

        public ActionResult Partial_Item_Cart()
        {
            var cart = db.shoppingCarts.Where(x => x.IdUser == User.Identity.Name && x.IsDelete == false).ToList();
            if (cart != null && cart.Any())
            {
                return PartialView(cart);
            }
            return PartialView();
        }
        public ActionResult Partial_Item_Cart_Atrri(int id, int iditem)
        {
            var items = db.ProductAttris.Where(x => x.ProductId == id).ToList();
            ViewBag.Iditem = iditem;
            return PartialView(items);
        }
        [HttpPost]
        public ActionResult UpdateColorSize(int id, string color, string size)
        {
            var cart = db.shoppingCarts.Where(x => x.IdUser == User.Identity.Name && x.IsDelete == false).ToList();
            if (cart != null)
            {

                var checkExits = cart.SingleOrDefault(x => x.Id == id);
                if (checkExits != null)
                {
                    checkExits.Color = color;
                    checkExits.Size = size;
                }
                db.shoppingCarts.Attach(checkExits);
                db.Entry(checkExits).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }
        [HttpPost]
        public ActionResult GetSizesByColor(string color)
        {
            var sizes = db.ProductAttris.Where(p => p.Alias == color && p.Quantity > 0)
                .Select(p => p.Size)
                .Distinct()
                .ToList();
            return Json(new { Sizes = sizes });
        }
        public ActionResult ShowCount()
        {
            var cart = db.shoppingCarts.Where(x => x.IdUser == User.Identity.Name && x.IsDelete == false).ToList();
            if (cart != null && cart.Any())
            {
                return Json(new { Count = cart.Count }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Count = 0 }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Update(int id, int quantity)
        {
            var cart = db.shoppingCarts.Where(x => x.IdUser == User.Identity.Name && x.IsDelete == false).ToList();
            if (cart != null)
            {

                var checkExits = cart.SingleOrDefault(x => x.ProductId == id);
                if (checkExits != null)
                {
                    checkExits.Quantity = quantity;
                    checkExits.TotalPrice = checkExits.Price * checkExits.Quantity;
                }
                db.shoppingCarts.Attach(checkExits);
                db.Entry(checkExits).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }
        [HttpPost]
        public ActionResult UpdateIsActive(int id, bool isactive)
        {
            var cart = db.shoppingCarts.Where(x => x.IdUser == User.Identity.Name && x.IsDelete == false).ToList();
            if (cart != null)
            {

                var checkExits = cart.SingleOrDefault(x => x.Id == id);
                if (checkExits != null)
                {
                    checkExits.IsActive = isactive;
                }
                db.shoppingCarts.Attach(checkExits);
                db.Entry(checkExits).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { Success = true, isactive = isactive });
            }
            return Json(new { Success = false });
        }
        public ActionResult GetTotalPriceIsActive()
        {
            var cart = db.shoppingCarts.Where(x => x.IdUser == User.Identity.Name && x.IsActive == true && x.IsDelete == false).ToList();
            if (cart != null && cart.Any())
            {
                var count = 0;
                var totalprice = decimal.Zero;
                foreach (var item in cart)
                {
                    count++;
                    totalprice += (item.TotalPrice);
                }

                return Json(new { Count = count, totalPrice = totalprice }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Count = 0, totalPrice = 0 }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateIsActiveAll(bool isactive)
        {
            var cart = db.shoppingCarts.Where(x => x.IdUser == User.Identity.Name && x.IsDelete == false).ToList();
            if (cart != null)
            {
                foreach (var item in cart)
                {
                    item.IsActive = isactive;
                    db.shoppingCarts.Attach(item);
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;

                }
                db.SaveChanges();
                return Json(new { Success = true, isactive = isactive });
            }
            return Json(new { Success = false });
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };
            var cart = db.shoppingCarts.Where(x => x.IdUser == User.Identity.Name && x.IsDelete == false).ToList();
            if (cart != null)
            {
                var checkProduct = cart.FirstOrDefault(x => x.ProductId == id);
                if (checkProduct != null)
                {
                    db.shoppingCarts.Remove(checkProduct);
                    db.SaveChanges();
                    code = new { Success = true, msg = "", code = 1, Count = cart.Count };
                }
            }
            return Json(code);
        }
        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        int number;
                        if (int.TryParse(item, out number))
                        {
                            var obj = db.shoppingCarts.Find(number);
                            db.shoppingCarts.Remove(obj);
                            db.SaveChanges();
                        }

                    }
                }
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }
        public ActionResult VnpayReturn()
        {
            if (Request.QueryString.Count > 0)
            {
                string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Chuoi bi mat
                var vnpayData = Request.QueryString;
                VnPayLibrary vnpay = new VnPayLibrary();

                foreach (string s in vnpayData)
                {
                    //get all querystring data
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }
                //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
                //vnp_TransactionNo: Ma GD tai he thong VNPAY
                //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
                //vnp_SecureHash: HmacSHA512 cua du lieu tra ve

                string orderCode = Convert.ToString(vnpay.GetResponseData("vnp_TxnRef"));
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                String vnp_SecureHash = Request.QueryString["vnp_SecureHash"];
                String TerminalID = Request.QueryString["vnp_TmnCode"];
                long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                String bankCode = Request.QueryString["vnp_BankCode"];

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                    {
                        var itemOrder = db.Orders.FirstOrDefault(x => x.Code == orderCode);
                        if (itemOrder != null)
                        {
                            itemOrder.StatusPayment = 2;//đã thanh toán
                            db.Orders.Attach(itemOrder);
                            db.Entry(itemOrder).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }
                        //Thanh toan thanh cong
                        ViewBag.InnerText = "Giao dịch được thực hiện thành công. Cảm ơn quý khách đã sử dụng dịch vụ";
                        //log.InfoFormat("Thanh toan thanh cong, OrderId={0}, VNPAY TranId={1}", orderId, vnpayTranId);
                    }
                    else
                    {
                        //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                        ViewBag.InnerText = "Có lỗi xảy ra trong quá trình xử lý.Mã lỗi: " + vnp_ResponseCode;
                        //log.InfoFormat("Thanh toan loi, OrderId={0}, VNPAY TranId={1},ResponseCode={2}", orderId, vnpayTranId, vnp_ResponseCode);
                        var itemOrder = db.Orders.FirstOrDefault(x => x.Code == orderCode);
                        if (itemOrder != null)
                        {
                            itemOrder.StatusPayment = 5;//H
                            db.Orders.Attach(itemOrder);
                            db.Entry(itemOrder).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                    //displayTmnCode.InnerText = "Mã Website (Terminal ID):" + TerminalID;
                    //displayTxnRef.InnerText = "Mã giao dịch thanh toán:" + orderId.ToString();
                    //displayVnpayTranNo.InnerText = "Mã giao dịch tại VNPAY:" + vnpayTranId.ToString();
                    //displayAmount.InnerText = "Số tiền thanh toán (VND):" + vnp_Amount.ToString();
                    //displayBankCode.InnerText = "Ngân hàng thanh toán:" + bankCode;
                    ViewBag.ThanhToanThanhCong = "Số tiền thanh toán (VND):" + vnp_Amount.ToString();
                }
                else
                {
                    //log.InfoFormat("Invalid signature, InputData={0}", Request.RawUrl);
                    ViewBag.InnerText = "Có lỗi xảy ra trong quá trình xử lý";
                    var itemOrder = db.Orders.FirstOrDefault(x => x.Code == orderCode);
                    if (itemOrder != null)
                    {
                        itemOrder.StatusPayment = 5;//H
                        db.Orders.Attach(itemOrder);
                        db.Entry(itemOrder).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }

            return View();
        }
        public ActionResult CheckOut()
        {

            var cart = db.shoppingCarts.Where(x => x.IdUser == User.Identity.Name && x.IsDelete == false).ToList();
            if (cart != null && cart.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }
        public ActionResult CheckOutSuccess()
        {
            return View();
        }
        public ActionResult CheckShoppingCartNotEmpty()
        {
            var cart = db.shoppingCarts.Where(x => x.IdUser == User.Identity.Name && x.IsDelete == false).ToList();
            if (cart != null && cart.Any())
            {
                var checkCart = cart.FirstOrDefault(x => x.IsActive);
                if (checkCart != null)
                {
                    return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Partial_Item_ThanhToan()
        {
            var cart = db.shoppingCarts.Where(x => x.IdUser == User.Identity.Name && x.IsActive == true && x.IsDelete == false).ToList();
            if (cart != null && cart.Any())
            {
                return PartialView(cart);
            }
            return PartialView();
        }
        public ActionResult Partial_CheckOut()
        {
            var iuser = User.Identity.GetUserId();
            var item = db.ProfileCustomers.FirstOrDefault(x => x.UserId == iuser);
            return PartialView(item);
        }
        public ActionResult _ChildPartial_CheckOut()
        {
            var iuser = User.Identity.GetUserId();
            var item = db.ProfileCustomers.FirstOrDefault(x => x.UserId == iuser);
            if (item != null)
            {
                return PartialView(item);
            }
            return PartialView();
        }
        public void ClearCart(List<ShoppingCart> carts, string code)
        {
            if (carts != null)
            {
                foreach (var item in carts)
                {
                    db.shoppingCarts.Attach(item);
                    item.IsDelete = true;
                    item.Code = code;
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
            }
        }
        public ActionResult MinusQuantityAttri(List<int> ids, List<string> colors, List<string> sizes, List<int> quantitys)
        {
            bool check = false;
            for (int i = 0; i < ids.Count; i++)
            {

            }
            if (check)
                return Json(new { Success = true });
            return Json(new { Success = false });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOutCart()
        {
            var iuser = User.Identity.GetUserId();
            var addProAddress = new AddressCustomer();
            var code = new { Success = false, Code = -1, Url = "" };
            if (ModelState.IsValid)
            {
                var profile = db.ProfileCustomers.FirstOrDefault(x => x.UserId == iuser);
                foreach (var itempro in profile.AddressCustomers)
                {
                    if (itempro.IsDefault)
                        addProAddress = itempro;
                }
                var cart = db.shoppingCarts.Where(x => x.IdUser == User.Identity.Name && x.IsActive == true && x.IsDelete == false).ToList();
                if (cart != null && cart.Any())
                {
                    Order order = new Order();
                    order.CustomerId = iuser;
                    order.CustomerName = addProAddress.Name;
                    order.Phone = addProAddress.PhoneNumber;
                    order.Email = addProAddress.Email;
                    order.Address = addProAddress.Address;
                    if(profile.TypePayment == 1)
                    {
                        order.StatusPayment = 2;
                    } 
                    else
                    {
                        order.StatusPayment = 1;//chưa thanh toán, 2/đã thanh toán ,3/ hoàn thành,4/hủy
                    }
                    order.StatusOrder = 1; // 1,/Chờ xác nhận  ,2/ chờ giao hàng, 3/ Hoàn thành, 4/trả hàng/hoàn tiền ,5/ hủy
                    cart.ForEach(x => order.OrderDetails.Add(new OrderDetail
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        Price = x.Price
                    }));
                    order.TotalAmount = cart.Sum(x => (x.Price * x.Quantity));
                    order.TypePayment = profile.TypePayment;
                    order.CreatedDate = DateTime.Now;
                    order.ModifedrDate = DateTime.Now;
                    order.CreatedBy = User.Identity.Name;
                    Random rd = new Random();
                    order.Code = "DH" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
                    db.Orders.Add(order);
                    db.SaveChanges();
                    //sendMail
                    var strSanpham = "";
                    var thanhtien = decimal.Zero;
                    var TongTien = decimal.Zero;
                    foreach (var sp in cart)
                    {
                        var item = db.ProductAttris.FirstOrDefault(x => x.ProductId == sp.ProductId && x.Size == sp.Size && x.Color == sp.Color);
                        if (item != null)
                        {
                            item.Quantity -= sp.Quantity;
                            db.ProductAttris.Attach(item);
                            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                    foreach (var sp in cart)
                    {
                        strSanpham += "<tr>";
                        strSanpham += "<td>" + sp.ProductName + "</td>";
                        strSanpham += "<td>" + sp.Quantity + "</td>";
                        strSanpham += "<td>" + T2NSHOP.Common.Common.FormatNumber(sp.TotalPrice, 0) + "</td>";
                        strSanpham += "</tr>";
                        thanhtien += sp.Price * sp.Quantity;
                    }
                    TongTien = thanhtien;
                    string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/send2.html"));
                    contentCustomer = contentCustomer.Replace("{{MaDon}}", order.Code);
                    contentCustomer = contentCustomer.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyy"));
                    contentCustomer = contentCustomer.Replace("{{SanPham}}", strSanpham);
                    contentCustomer = contentCustomer.Replace("{{TenKhachHang}}", order.CustomerName);
                    contentCustomer = contentCustomer.Replace("{{Phone}}", order.Phone);
                    contentCustomer = contentCustomer.Replace("{{Email}}", order.Email);
                    contentCustomer = contentCustomer.Replace("{{DiaChiNhanHang}}", order.Address);
                    contentCustomer = contentCustomer.Replace("{{ThanhTien}}", T2NSHOP.Common.Common.FormatNumber(thanhtien, 0));
                    contentCustomer = contentCustomer.Replace("{{TongTien}}", T2NSHOP.Common.Common.FormatNumber(TongTien, 0));
                    T2NSHOP.Common.Common.SendMail("ShopOnLine", "Đơn Hàng #" + order.Code, contentCustomer.ToString(), order.Email);

                    string contentAdmin = System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/send1.html"));
                    contentAdmin = contentAdmin.Replace("{{MaDon}}", order.Code);
                    contentAdmin = contentAdmin.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyy"));
                    contentAdmin = contentAdmin.Replace("{{SanPham}}", strSanpham);
                    contentAdmin = contentAdmin.Replace("{{TenKhachHang}}", order.CustomerName);
                    contentAdmin = contentAdmin.Replace("{{Phone}}", order.Phone);
                    contentAdmin = contentAdmin.Replace("{{Email}}", order.Email);
                    contentAdmin = contentAdmin.Replace("{{DiaChiNhanHang}}", order.Address);
                    contentAdmin = contentAdmin.Replace("{{ThanhTien}}", T2NSHOP.Common.Common.FormatNumber(thanhtien, 0));
                    contentAdmin = contentAdmin.Replace("{{TongTien}}", T2NSHOP.Common.Common.FormatNumber(TongTien, 0));
                    T2NSHOP.Common.Common.SendMail("ShopOnLine", "Đơn Hàng mới #" + order.Code, contentAdmin.ToString(), ConfigurationManager.AppSettings["EmailAdmin"]);
                    ClearCart(cart, order.Code);
                    code = new { Success = true, Code = profile.TypePayment, Url = "" };
                    if (profile.TypePayment == 2)
                    {
                        var url = UrlPayment(profile.TypePaymentVN, order.Code);
                        code = new { Success = true, Code = profile.TypePayment, Url = url };
                    }


                    //return RedirectToAction("CheckOutSuccess");
                }
            }
            return Json(code);
        }
        #region
        public string UrlPayment(int TypePaymentVN, string orderCode)
        {
            var urlPayment = "";
            var order = db.Orders.FirstOrDefault(x => x.Code == orderCode);
            //Get Config Info
            string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"]; //URL nhan ket qua tra ve 
            string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"]; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"]; //Ma định danh merchant kết nối (Terminal Id)
            string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Secret Key

            VnPayLibrary vnpay = new VnPayLibrary();
            var Price = (long)order.TotalAmount * 100;
            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", Price.ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            if (TypePaymentVN == 1)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");
            }
            else if (TypePaymentVN == 2)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            }
            else if (TypePaymentVN == 3)
            {
                vnpay.AddRequestData("vnp_BankCode", "INTCARD");
            }

            vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toán đơn hàng:" + order.Code);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.Code); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            //Add Params of 2.1.0 Version
            //Billing

            urlPayment = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            //log.InfoFormat("VNPAY URL: {0}", paymentUrl);
            return urlPayment;
        }
        #endregion
        //

        //[HttpPost]
        //public ActionResult DeleteAll()
        //{
        //    ShoppingCart cart = (ShoppingCart)Session["Cart"];
        //    if (cart != null)
        //    {
        //        cart.ClearCart();
        //        return Json(new { Success = true });
        //    }
        //    return Json(new { Success = false });
        //}
    }
}