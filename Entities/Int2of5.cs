using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public static class Int2of5
    {
        private static string Encode(string Data)
        {
            try
            {
                //0 = thin
                //1 = thick
                IDictionary<int, string> NumbersMapping = new Dictionary<int, string>();
                NumbersMapping.Add(0, "00110");
                NumbersMapping.Add(1, "10001");
                NumbersMapping.Add(2, "01001");
                NumbersMapping.Add(3, "11000");
                NumbersMapping.Add(4, "00101");
                NumbersMapping.Add(5, "10100");
                NumbersMapping.Add(6, "01100");
                NumbersMapping.Add(7, "00011");
                NumbersMapping.Add(8, "10010");
                NumbersMapping.Add(9, "01010");

                if (string.IsNullOrEmpty(Data)) throw new Exception("No data received");
                if (!Data.All(char.IsDigit)) throw new Exception("Only numbers are accepted");
                if (Data.Length % 2 != 0) throw new Exception("Number os digits have to be even");

                IList<KeyValuePair<int, string>> Digits = new List<KeyValuePair<int, string>>();
                for (int i = 0; i < Data.Length; i++)
                {
                    int key = Convert.ToInt32(Data[i].ToString());
                    string value = NumbersMapping[Convert.ToInt32(Data[i].ToString())];

                    Digits.Add(new KeyValuePair<int, string>(Convert.ToInt32(Data[i].ToString()),
                               NumbersMapping[Convert.ToInt32(Data[i].ToString())]));
                }

                string Result = string.Empty;
                for (int i = 0; i < Digits.Count; i += 2)
                {
                    string Pair1 = Digits[i].Value;
                    string Pair2 = Digits[i + 1].Value;

                    //Pair 1 e 2 will get interleaved
                    //Pair 1 = will be bars
                    //Pair 2 = will be spaces
                    //Pseudo-codes:
                    //A = thin space
                    //B = thick space
                    //X = thin bar
                    //Y = thick bar
                    for (int j = 0; j < 5; j++)
                        Result += (Pair1[j].ToString() == "0" ? "X" : "Y") +
                                  (Pair2[j].ToString() == "0" ? "A" : "B");
                }

                //Append start and ending
                return "XAXA" + Result + "YAX";
            }
            catch (Exception ex)
            {
                return "#" + ex.Message;
            }
        }

        public static Image GenerateBarCode(string Data, int Width, int Height, int ScaleFactor)
        {
            try
            {
                string EncodedData = Encode(Data);
                if (string.IsNullOrEmpty(EncodedData))
                    throw new Exception("Encoding process returned empty");
                if (EncodedData[0].ToString() == "#") throw new Exception(EncodedData);

                int Position = 20, ThinWidth = 1 * ScaleFactor, ThickWidth = 3 * ScaleFactor;
                Image img = new System.Drawing.Bitmap(Width, Height);
                using (Graphics gr = Graphics.FromImage(img))
                {
                    //Initial white color filling
                    gr.FillRectangle(Brushes.White, 0, 0, Width, Height);

                    for (int i = 0; i < EncodedData.Length; i++)
                    {
                        //Replace the pseudo-codes with bars or spaces
                        switch (EncodedData[i].ToString())
                        {
                            case "A":
                                gr.FillRectangle(System.Drawing.Brushes.White,
                                                 Position, 0, ThinWidth, Height);
                                Position += ThinWidth;
                                break;
                            case "B":
                                gr.FillRectangle(System.Drawing.Brushes.White,
                                                 Position, 0, ThickWidth, Height);
                                Position += ThickWidth;
                                break;
                            case "X":
                                gr.FillRectangle(System.Drawing.Brushes.Black,
                                                 Position, 0, ThinWidth, Height);
                                Position += ThinWidth;
                                break;
                            case "Y":
                                gr.FillRectangle(System.Drawing.Brushes.Black,
                                                 Position, 0, ThickWidth, Height);
                                Position += ThickWidth;
                                break;
                        }
                    }
                    return img;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
