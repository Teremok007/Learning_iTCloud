using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Net_module1_4_1_lab
{
    class OnlineShop
    {

        // 4) declare event of type EventHandler<GoodsInfoEventArgs>
        public delegate void ShopEventHandler(object sender, GoodsInfoEventArgs e);
        public event ShopEventHandler NewGoodsInfo;

        // 5) declare method NewGoods for event initiation
        // use parameter string to get GoodsName
        public void NewGoods(string GoodName)
        {

            // don't forget to check if event is not null
            // in true case intiate the event
            // use next line
            if (!String.IsNullOrEmpty(GoodName))
            {
                NewGoodsInfo?.Invoke(this, new GoodsInfoEventArgs(GoodName));
            }
        }
        // your_event_name(this, new GoodsInfoEventArgs(GoodsName));

    }
}
