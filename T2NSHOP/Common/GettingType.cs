namespace T2NSHOP.Common
{
    public class GettingType
    {
        public static string GetStatusPayment(int key)
        {
            if (key == 1)
            {
                return "Chờ thanh toán";
            }
            else
            {
                if (key == 2)
                {
                    return "Đã thanh toán";
                }
                else
                {
                    if (key == 3)
                    {
                        return "Hoàn thành";
                    }
                    else
                    {
                        if (key == 4)
                        {
                            return "Hoàn tiền";
                        }
                    }
                }
            }
            return "Hủy";
        }
        public static string GetStatusOrder(int key)
        {
            if (key == 1)
            {
                return "Chờ xác nhận";
            }
            else
            {
                if (key == 2)
                {
                    return "Chờ giao hàng";
                }
                else
                {
                    if (key == 3)
                    {
                        return "Hoàn thành";
                    }
                    else
                    {
                        if (key == 4)
                        {
                            return "Trả hàng";
                        }
                    }
                }
            }
            return "Hủy";
        }

    }
}