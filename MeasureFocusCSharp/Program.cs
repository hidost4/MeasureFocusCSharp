using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OpenCvSharp;

namespace MeasureFocusCSharp
{
    class Program
    {
        static public double OCV_FocusMeasure(Mat src)
        {
            Mat dst = new Mat();
            Mat src_gray = new Mat();            

            if (src.Channels() == 1)//Mono
            {
                Cv2.Laplacian(src, dst, MatType.CV_64FC1);
            }
            else//RGB
            {
                Cv2.CvtColor(src, src_gray, ColorConversionCodes.BGR2GRAY);// Convert the image to grayscale
                Cv2.Laplacian(src_gray, dst, MatType.CV_64FC1);
            }

            Cv2.MeanStdDev(dst, out var mean, out var stddev);

            return stddev.Val0 * stddev.Val0;

        }

        static void Main(string[] args)
        {

            Mat OrgImage, Test1, Test2, Test3, Test4;
            double dValue0, dValue1, dValue2, dValue3, dValue4;            

            OrgImage = Cv2.ImRead("Org.bmp");//Load Org Image
            Test1 = Cv2.ImRead("Test1.bmp");//Load Test1 Image
            Test2 = Cv2.ImRead("Test2.bmp");//Load Test2 Image
            Test3 = Cv2.ImRead("Test3.bmp");//Load Test3 Image
            Test4 = Cv2.ImRead("Test4.bmp");//Load Test4 Image


            dValue0 = OCV_FocusMeasure(OrgImage);
            dValue1 = OCV_FocusMeasure(Test1);
            dValue2 = OCV_FocusMeasure(Test2);
            dValue3 = OCV_FocusMeasure(Test3);
            dValue4 = OCV_FocusMeasure(Test4);            

            Cv2.ImShow(string.Format("OrgImage({0:f})", dValue0), OrgImage);
            Cv2.ImShow(string.Format("Test1({0:f})", dValue1), Test1);
            Cv2.ImShow(string.Format("Test2({0:f})", dValue2), Test2);
            Cv2.ImShow(string.Format("Test3({0:f})", dValue3), Test3);
            Cv2.ImShow(string.Format("Test4({0:f})", dValue4), Test4);

            Cv2.WaitKey(0);
        }

        

    }
}
