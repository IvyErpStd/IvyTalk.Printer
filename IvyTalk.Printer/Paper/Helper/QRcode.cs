﻿using System.Drawing;
using ZXing;
using ZXing.Common;

namespace IvyTalk.Printer.Paper.Helper
{
    class QRCode
    {
        public QRCode() { }

        public Bitmap grant_one_code(string num_str, int w, int h, bool showNum)
        {
            // 1.设置条形码规格
            EncodingOptions encodeOption = new EncodingOptions();
            encodeOption.Width = w;
            encodeOption.Height = h; // 必须制定高度、宽度 
            encodeOption.PureBarcode = !showNum;

            // 2.生成条形码图片并保存
            ZXing.BarcodeWriter wr = new BarcodeWriter();
            wr.Options = encodeOption;
            wr.Format = BarcodeFormat.CODE_128;//.EAN_13; //  条形码规格：EAN13规格：12（无校验位）或13位数字
            Bitmap img = wr.Write(num_str); // 生成图片
            return img;
        }

        public Bitmap grant_qrcode(string num_str, int w, int h)
        {
            // 1.设置QR二维码的规格
            ZXing.QrCode.QrCodeEncodingOptions qrEncodeOption = new ZXing.QrCode.QrCodeEncodingOptions();
            qrEncodeOption.CharacterSet = "UTF-8"; // 设置编码格式，否则读取'中文'乱码
            qrEncodeOption.Height = h;
            qrEncodeOption.Width = w;
            qrEncodeOption.Margin = 1; // 设置周围空白边距

            // 2.生成条形码图片并保存
            ZXing.BarcodeWriter wr = new BarcodeWriter();
            wr.Format = BarcodeFormat.QR_CODE; // 二维码
            wr.Options = qrEncodeOption;
            Bitmap img = wr.Write(num_str);
            return img;
        }
    }
}
