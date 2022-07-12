using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LICENSE
{
    public class Decoder
    {
        private string str5 = "";
        private string tus;

        public string decryptData(string pass)
        {
            string str1 = "";
            string str2 = "";
            string str3 = pass.Substring(0, 2);
            for (int startIndex = 0; startIndex < 2; ++startIndex)
            {
                this.tus = str3.Substring(startIndex, 1);
                this.g1De();
                str1 += this.str5;
            }
            int int32;
            try
            {
                int32 = Convert.ToInt32(str1);
            }
            catch (Exception ex)
            {
                return "";
            }
            int num1;
            if (int32 > 6)
            {
                num1 = int32 - int32 / 6 * 6;
                if (num1 == 0)
                    num1 = 6;
            }
            else
                num1 = int32;
            int num2 = 0;
            this.str5 = "";
            for (int startIndex = 0; startIndex < pass.Length; ++startIndex)
            {
                this.tus = pass.Substring(startIndex, 1);
                ++num2;
                if (num2 == 7)
                    num2 = 1;
                if (startIndex == 1)
                    num2 = 1;
                if (num1 == 1)
                {
                    switch (num2)
                    {
                        case 1:
                            this.g1De();
                            break;

                        case 2:
                            this.g2De();
                            break;

                        case 3:
                            this.g3De();
                            break;

                        case 4:
                            this.g4De();
                            break;

                        case 5:
                            this.g5De();
                            break;

                        case 6:
                            this.g6De();
                            break;
                    }
                }
                if (num1 == 2)
                {
                    switch (num2)
                    {
                        case 1:
                            this.g1De();
                            break;

                        case 2:
                            this.g5De();
                            break;

                        case 3:
                            this.g4De();
                            break;

                        case 4:
                            this.g6De();
                            break;

                        case 5:
                            this.g2De();
                            break;

                        case 6:
                            this.g3De();
                            break;
                    }
                }
                if (num1 == 3)
                {
                    switch (num2)
                    {
                        case 1:
                            this.g1De();
                            break;

                        case 2:
                            this.g4De();
                            break;

                        case 3:
                            this.g2De();
                            break;

                        case 4:
                            this.g5De();
                            break;

                        case 5:
                            this.g6De();
                            break;

                        case 6:
                            this.g3De();
                            break;
                    }
                }
                if (num1 == 4)
                {
                    switch (num2)
                    {
                        case 1:
                            this.g1De();
                            break;

                        case 2:
                            this.g6De();
                            break;

                        case 3:
                            this.g5De();
                            break;

                        case 4:
                            this.g3De();
                            break;

                        case 5:
                            this.g4De();
                            break;

                        case 6:
                            this.g2De();
                            break;
                    }
                }
                if (num1 == 5)
                {
                    switch (num2)
                    {
                        case 1:
                            this.g1De();
                            break;

                        case 2:
                            this.g3De();
                            break;

                        case 3:
                            this.g6De();
                            break;

                        case 4:
                            this.g2De();
                            break;

                        case 5:
                            this.g5De();
                            break;

                        case 6:
                            this.g4De();
                            break;
                    }
                }
                if (num1 == 6)
                {
                    switch (num2)
                    {
                        case 1:
                            this.g1De();
                            break;

                        case 2:
                            this.g6De();
                            break;

                        case 3:
                            this.g2De();
                            break;

                        case 4:
                            this.g4De();
                            break;

                        case 5:
                            this.g3De();
                            break;

                        case 6:
                            this.g5De();
                            break;
                    }
                }
                str2 += this.str5;
            }
            return str2;
        }

        private void g1De()
        {
            if (this.tus == "0")
                this.str5 = "A";
            if (this.tus == "9")
                this.str5 = "B";
            if (this.tus == "8")
                this.str5 = "C";
            if (this.tus == "7")
                this.str5 = "D";
            if (this.tus == "6")
                this.str5 = "E";
            if (this.tus == "5")
                this.str5 = "F";
            if (this.tus == "4")
                this.str5 = "G";
            if (this.tus == "3")
                this.str5 = "H";
            if (this.tus == "2")
                this.str5 = "I";
            if (this.tus == "1")
                this.str5 = "J";
            if (this.tus == "z")
                this.str5 = "K";
            if (this.tus == "y")
                this.str5 = "L";
            if (this.tus == "x")
                this.str5 = "M";
            if (this.tus == "w")
                this.str5 = "N";
            if (this.tus == "v")
                this.str5 = "O";
            if (this.tus == "u")
                this.str5 = "P";
            if (this.tus == "a")
                this.str5 = "Q";
            if (this.tus == "s")
                this.str5 = "R";
            if (this.tus == "r")
                this.str5 = "S";
            if (this.tus == "q")
                this.str5 = "T";
            if (this.tus == "p")
                this.str5 = "U";
            if (this.tus == "o")
                this.str5 = "V";
            if (this.tus == "n")
                this.str5 = "W";
            if (this.tus == "m")
                this.str5 = "X";
            if (this.tus == "l")
                this.str5 = "Y";
            if (this.tus == "k")
                this.str5 = "Z";
            if (this.tus == "j")
                this.str5 = "1";
            if (this.tus == "i")
                this.str5 = "2";
            if (this.tus == "h")
                this.str5 = "3";
            if (this.tus == "g")
                this.str5 = "4";
            if (this.tus == "f")
                this.str5 = "5";
            if (this.tus == "e")
                this.str5 = "6";
            if (this.tus == "d")
                this.str5 = "7";
            if (this.tus == "c")
                this.str5 = "8";
            if (this.tus == "b")
                this.str5 = "9";
            if (this.tus == "t")
                this.str5 = "0";
            if (!(this.tus == "-"))
                return;
            this.str5 = "-";
        }

        private void g2De()
        {
            if (this.tus == "8")
                this.str5 = "A";
            if (this.tus == "4")
                this.str5 = "B";
            if (this.tus == "0")
                this.str5 = "C";
            if (this.tus == "9")
                this.str5 = "D";
            if (this.tus == "7")
                this.str5 = "E";
            if (this.tus == "3")
                this.str5 = "F";
            if (this.tus == "1")
                this.str5 = "G";
            if (this.tus == "6")
                this.str5 = "H";
            if (this.tus == "5")
                this.str5 = "I";
            if (this.tus == "2")
                this.str5 = "J";
            if (this.tus == "o")
                this.str5 = "K";
            if (this.tus == "i")
                this.str5 = "L";
            if (this.tus == "u")
                this.str5 = "M";
            if (this.tus == "y")
                this.str5 = "N";
            if (this.tus == "t")
                this.str5 = "O";
            if (this.tus == "r")
                this.str5 = "P";
            if (this.tus == "e")
                this.str5 = "Q";
            if (this.tus == "w")
                this.str5 = "R";
            if (this.tus == "q")
                this.str5 = "S";
            if (this.tus == "z")
                this.str5 = "T";
            if (this.tus == "x")
                this.str5 = "U";
            if (this.tus == "c")
                this.str5 = "V";
            if (this.tus == "v")
                this.str5 = "W";
            if (this.tus == "b")
                this.str5 = "X";
            if (this.tus == "n")
                this.str5 = "Y";
            if (this.tus == "m")
                this.str5 = "Z";
            if (this.tus == "p")
                this.str5 = "1";
            if (this.tus == "a")
                this.str5 = "2";
            if (this.tus == "s")
                this.str5 = "3";
            if (this.tus == "d")
                this.str5 = "4";
            if (this.tus == "f")
                this.str5 = "5";
            if (this.tus == "g")
                this.str5 = "6";
            if (this.tus == "h")
                this.str5 = "7";
            if (this.tus == "j")
                this.str5 = "8";
            if (this.tus == "k")
                this.str5 = "9";
            if (this.tus == "l")
                this.str5 = "0";
            if (!(this.tus == "-"))
                return;
            this.str5 = "-";
        }

        private void g3De()
        {
            if (this.tus == "1")
                this.str5 = "A";
            if (this.tus == "3")
                this.str5 = "B";
            if (this.tus == "5")
                this.str5 = "C";
            if (this.tus == "7")
                this.str5 = "D";
            if (this.tus == "9")
                this.str5 = "E";
            if (this.tus == "2")
                this.str5 = "F";
            if (this.tus == "4")
                this.str5 = "G";
            if (this.tus == "6")
                this.str5 = "H";
            if (this.tus == "8")
                this.str5 = "I";
            if (this.tus == "0")
                this.str5 = "J";
            if (this.tus == "h")
                this.str5 = "K";
            if (this.tus == "g")
                this.str5 = "L";
            if (this.tus == "f")
                this.str5 = "M";
            if (this.tus == "d")
                this.str5 = "N";
            if (this.tus == "s")
                this.str5 = "O";
            if (this.tus == "a")
                this.str5 = "P";
            if (this.tus == "p")
                this.str5 = "Q";
            if (this.tus == "o")
                this.str5 = "R";
            if (this.tus == "i")
                this.str5 = "S";
            if (this.tus == "u")
                this.str5 = "T";
            if (this.tus == "y")
                this.str5 = "U";
            if (this.tus == "t")
                this.str5 = "V";
            if (this.tus == "r")
                this.str5 = "W";
            if (this.tus == "e")
                this.str5 = "X";
            if (this.tus == "w")
                this.str5 = "Y";
            if (this.tus == "j")
                this.str5 = "Z";
            if (this.tus == "q")
                this.str5 = "1";
            if (this.tus == "k")
                this.str5 = "2";
            if (this.tus == "l")
                this.str5 = "3";
            if (this.tus == "z")
                this.str5 = "4";
            if (this.tus == "x")
                this.str5 = "5";
            if (this.tus == "c")
                this.str5 = "6";
            if (this.tus == "v")
                this.str5 = "7";
            if (this.tus == "b")
                this.str5 = "8";
            if (this.tus == "n")
                this.str5 = "9";
            if (this.tus == "m")
                this.str5 = "0";
            if (!(this.tus == "-"))
                return;
            this.str5 = "-";
        }

        private void g4De()
        {
            if (this.tus == "q")
                this.str5 = "A";
            if (this.tus == "f")
                this.str5 = "B";
            if (this.tus == "h")
                this.str5 = "C";
            if (this.tus == "t")
                this.str5 = "D";
            if (this.tus == "s")
                this.str5 = "E";
            if (this.tus == "i")
                this.str5 = "F";
            if (this.tus == "o")
                this.str5 = "G";
            if (this.tus == "n")
                this.str5 = "H";
            if (this.tus == "5")
                this.str5 = "I";
            if (this.tus == "e")
                this.str5 = "J";
            if (this.tus == "w")
                this.str5 = "K";
            if (this.tus == "c")
                this.str5 = "L";
            if (this.tus == "l")
                this.str5 = "M";
            if (this.tus == "u")
                this.str5 = "N";
            if (this.tus == "g")
                this.str5 = "O";
            if (this.tus == "6")
                this.str5 = "P";
            if (this.tus == "x")
                this.str5 = "Q";
            if (this.tus == "0")
                this.str5 = "R";
            if (this.tus == "m")
                this.str5 = "S";
            if (this.tus == "r")
                this.str5 = "T";
            if (this.tus == "k")
                this.str5 = "U";
            if (this.tus == "7")
                this.str5 = "V";
            if (this.tus == "p")
                this.str5 = "W";
            if (this.tus == "b")
                this.str5 = "X";
            if (this.tus == "1")
                this.str5 = "Y";
            if (this.tus == "3")
                this.str5 = "Z";
            if (this.tus == "j")
                this.str5 = "1";
            if (this.tus == "4")
                this.str5 = "2";
            if (this.tus == "d")
                this.str5 = "3";
            if (this.tus == "2")
                this.str5 = "4";
            if (this.tus == "v")
                this.str5 = "5";
            if (this.tus == "a")
                this.str5 = "6";
            if (this.tus == "9")
                this.str5 = "7";
            if (this.tus == "z")
                this.str5 = "8";
            if (this.tus == "8")
                this.str5 = "9";
            if (this.tus == "y")
                this.str5 = "0";
            if (!(this.tus == "-"))
                return;
            this.str5 = "-";
        }

        private void g5De()
        {
            if (this.tus == "v")
                this.str5 = "A";
            if (this.tus == "p")
                this.str5 = "B";
            if (this.tus == "k")
                this.str5 = "C";
            if (this.tus == "l")
                this.str5 = "D";
            if (this.tus == "y")
                this.str5 = "E";
            if (this.tus == "5")
                this.str5 = "F";
            if (this.tus == "h")
                this.str5 = "G";
            if (this.tus == "6")
                this.str5 = "H";
            if (this.tus == "g")
                this.str5 = "I";
            if (this.tus == "b")
                this.str5 = "J";
            if (this.tus == "7")
                this.str5 = "K";
            if (this.tus == "i")
                this.str5 = "L";
            if (this.tus == "d")
                this.str5 = "M";
            if (this.tus == "o")
                this.str5 = "N";
            if (this.tus == "1")
                this.str5 = "O";
            if (this.tus == "r")
                this.str5 = "P";
            if (this.tus == "4")
                this.str5 = "Q";
            if (this.tus == "u")
                this.str5 = "R";
            if (this.tus == "t")
                this.str5 = "S";
            if (this.tus == "3")
                this.str5 = "T";
            if (this.tus == "a")
                this.str5 = "U";
            if (this.tus == "2")
                this.str5 = "V";
            if (this.tus == "j")
                this.str5 = "W";
            if (this.tus == "0")
                this.str5 = "X";
            if (this.tus == "n")
                this.str5 = "Y";
            if (this.tus == "c")
                this.str5 = "Z";
            if (this.tus == "9")
                this.str5 = "1";
            if (this.tus == "f")
                this.str5 = "2";
            if (this.tus == "8")
                this.str5 = "3";
            if (this.tus == "e")
                this.str5 = "4";
            if (this.tus == "m")
                this.str5 = "5";
            if (this.tus == "q")
                this.str5 = "6";
            if (this.tus == "x")
                this.str5 = "7";
            if (this.tus == "z")
                this.str5 = "8";
            if (this.tus == "w")
                this.str5 = "9";
            if (this.tus == "s")
                this.str5 = "0";
            if (!(this.tus == "-"))
                return;
            this.str5 = "-";
        }

        private void g6De()
        {
            if (this.tus == "l")
                this.str5 = "A";
            if (this.tus == "5")
                this.str5 = "B";
            if (this.tus == "8")
                this.str5 = "C";
            if (this.tus == "m")
                this.str5 = "D";
            if (this.tus == "j")
                this.str5 = "E";
            if (this.tus == "7")
                this.str5 = "F";
            if (this.tus == "6")
                this.str5 = "G";
            if (this.tus == "b")
                this.str5 = "H";
            if (this.tus == "n")
                this.str5 = "I";
            if (this.tus == "4")
                this.str5 = "J";
            if (this.tus == "k")
                this.str5 = "K";
            if (this.tus == "9")
                this.str5 = "L";
            if (this.tus == "0")
                this.str5 = "M";
            if (this.tus == "a")
                this.str5 = "N";
            if (this.tus == "3")
                this.str5 = "O";
            if (this.tus == "2")
                this.str5 = "P";
            if (this.tus == "f")
                this.str5 = "Q";
            if (this.tus == "u")
                this.str5 = "R";
            if (this.tus == "r")
                this.str5 = "S";
            if (this.tus == "1")
                this.str5 = "T";
            if (this.tus == "c")
                this.str5 = "U";
            if (this.tus == "x")
                this.str5 = "V";
            if (this.tus == "e")
                this.str5 = "W";
            if (this.tus == "q")
                this.str5 = "X";
            if (this.tus == "z")
                this.str5 = "Y";
            if (this.tus == "i")
                this.str5 = "Z";
            if (this.tus == "w")
                this.str5 = "1";
            if (this.tus == "v")
                this.str5 = "2";
            if (this.tus == "s")
                this.str5 = "3";
            if (this.tus == "y")
                this.str5 = "4";
            if (this.tus == "p")
                this.str5 = "5";
            if (this.tus == "g")
                this.str5 = "6";
            if (this.tus == "t")
                this.str5 = "7";
            if (this.tus == "h")
                this.str5 = "8";
            if (this.tus == "o")
                this.str5 = "9";
            if (this.tus == "d")
                this.str5 = "0";
            if (!(this.tus == "-"))
                return;
            this.str5 = "-";
        }
    }
}
