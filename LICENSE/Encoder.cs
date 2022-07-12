using System;

namespace LICENSE
{
    public class Encoder
    {
        private string str5;
        private string str1;
        private string tus;

        public string EncryptData(string pass, string glic)
        {
            this.str1 = "";
            this.str5 = "";
            this.tus = "";
            int num1 = 0;
            int int32 = Convert.ToInt32(glic);
            int num2;
            if (int32 > 6)
            {
                num2 = int32 - int32 / 6 * 6;
                if (num2 == 0)
                    num2 = 6;
            }
            else
                num2 = int32;
            for (int startIndex = 0; startIndex < pass.Length; ++startIndex)
            {
                this.tus = pass.Substring(startIndex, 1);
                ++num1;
                if (startIndex == 1)
                    num1 = 1;
                if (num1 == 7)
                    num1 = 1;
                if (num2 == 1)
                {
                    switch (num1)
                    {
                        case 1:
                            this.g1En();
                            break;

                        case 2:
                            this.g2En();
                            break;

                        case 3:
                            this.g3En();
                            break;

                        case 4:
                            this.g4En();
                            break;

                        case 5:
                            this.g5En();
                            break;

                        case 6:
                            this.g6En();
                            break;
                    }
                }
                if (num2 == 2)
                {
                    switch (num1)
                    {
                        case 1:
                            this.g1En();
                            break;

                        case 2:
                            this.g5En();
                            break;

                        case 3:
                            this.g4En();
                            break;

                        case 4:
                            this.g6En();
                            break;

                        case 5:
                            this.g2En();
                            break;

                        case 6:
                            this.g3En();
                            break;
                    }
                }
                if (num2 == 3)
                {
                    switch (num1)
                    {
                        case 1:
                            this.g1En();
                            break;

                        case 2:
                            this.g4En();
                            break;

                        case 3:
                            this.g2En();
                            break;

                        case 4:
                            this.g5En();
                            break;

                        case 5:
                            this.g6En();
                            break;

                        case 6:
                            this.g3En();
                            break;
                    }
                }
                if (num2 == 4)
                {
                    switch (num1)
                    {
                        case 1:
                            this.g1En();
                            break;

                        case 2:
                            this.g6En();
                            break;

                        case 3:
                            this.g5En();
                            break;

                        case 4:
                            this.g3En();
                            break;

                        case 5:
                            this.g4En();
                            break;

                        case 6:
                            this.g2En();
                            break;
                    }
                }
                if (num2 == 5)
                {
                    switch (num1)
                    {
                        case 1:
                            this.g1En();
                            break;

                        case 2:
                            this.g3En();
                            break;

                        case 3:
                            this.g6En();
                            break;

                        case 4:
                            this.g2En();
                            break;

                        case 5:
                            this.g5En();
                            break;

                        case 6:
                            this.g4En();
                            break;
                    }
                }
                if (num2 == 6)
                {
                    switch (num1)
                    {
                        case 1:
                            this.g1En();
                            break;

                        case 2:
                            this.g6En();
                            break;

                        case 3:
                            this.g2En();
                            break;

                        case 4:
                            this.g4En();
                            break;

                        case 5:
                            this.g3En();
                            break;

                        case 6:
                            this.g5En();
                            break;
                    }
                }
                this.str1 += this.str5;
            }
            return this.str1;
        }

        public void g1En()
        {
            if (this.tus == "A")
                this.str5 = "0";
            if (this.tus == "B")
                this.str5 = "9";
            if (this.tus == "C")
                this.str5 = "8";
            if (this.tus == "D")
                this.str5 = "7";
            if (this.tus == "E")
                this.str5 = "6";
            if (this.tus == "F")
                this.str5 = "5";
            if (this.tus == "G")
                this.str5 = "4";
            if (this.tus == "H")
                this.str5 = "3";
            if (this.tus == "I")
                this.str5 = "2";
            if (this.tus == "J")
                this.str5 = "1";
            if (this.tus == "K")
                this.str5 = "z";
            if (this.tus == "L")
                this.str5 = "y";
            if (this.tus == "M")
                this.str5 = "x";
            if (this.tus == "N")
                this.str5 = "w";
            if (this.tus == "O")
                this.str5 = "v";
            if (this.tus == "P")
                this.str5 = "u";
            if (this.tus == "Q")
                this.str5 = "a";
            if (this.tus == "R")
                this.str5 = "s";
            if (this.tus == "S")
                this.str5 = "r";
            if (this.tus == "T")
                this.str5 = "q";
            if (this.tus == "U")
                this.str5 = "p";
            if (this.tus == "V")
                this.str5 = "o";
            if (this.tus == "W")
                this.str5 = "n";
            if (this.tus == "X")
                this.str5 = "m";
            if (this.tus == "Y")
                this.str5 = "l";
            if (this.tus == "Z")
                this.str5 = "k";
            if (this.tus == "1")
                this.str5 = "j";
            if (this.tus == "2")
                this.str5 = "i";
            if (this.tus == "3")
                this.str5 = "h";
            if (this.tus == "4")
                this.str5 = "g";
            if (this.tus == "5")
                this.str5 = "f";
            if (this.tus == "6")
                this.str5 = "e";
            if (this.tus == "7")
                this.str5 = "d";
            if (this.tus == "8")
                this.str5 = "c";
            if (this.tus == "9")
                this.str5 = "b";
            if (this.tus == "0")
                this.str5 = "t";
            if (!(this.tus == "-"))
                return;
            this.str5 = "-";
        }

        public void g2En()
        {
            if (this.tus == "A")
                this.str5 = "8";
            if (this.tus == "B")
                this.str5 = "4";
            if (this.tus == "C")
                this.str5 = "0";
            if (this.tus == "D")
                this.str5 = "9";
            if (this.tus == "E")
                this.str5 = "7";
            if (this.tus == "F")
                this.str5 = "3";
            if (this.tus == "G")
                this.str5 = "1";
            if (this.tus == "H")
                this.str5 = "6";
            if (this.tus == "I")
                this.str5 = "5";
            if (this.tus == "J")
                this.str5 = "2";
            if (this.tus == "K")
                this.str5 = "o";
            if (this.tus == "L")
                this.str5 = "i";
            if (this.tus == "M")
                this.str5 = "u";
            if (this.tus == "N")
                this.str5 = "y";
            if (this.tus == "O")
                this.str5 = "t";
            if (this.tus == "P")
                this.str5 = "r";
            if (this.tus == "Q")
                this.str5 = "e";
            if (this.tus == "R")
                this.str5 = "w";
            if (this.tus == "S")
                this.str5 = "q";
            if (this.tus == "T")
                this.str5 = "z";
            if (this.tus == "U")
                this.str5 = "x";
            if (this.tus == "V")
                this.str5 = "c";
            if (this.tus == "W")
                this.str5 = "v";
            if (this.tus == "X")
                this.str5 = "b";
            if (this.tus == "Y")
                this.str5 = "n";
            if (this.tus == "Z")
                this.str5 = "m";
            if (this.tus == "1")
                this.str5 = "p";
            if (this.tus == "2")
                this.str5 = "a";
            if (this.tus == "3")
                this.str5 = "s";
            if (this.tus == "4")
                this.str5 = "d";
            if (this.tus == "5")
                this.str5 = "f";
            if (this.tus == "6")
                this.str5 = "g";
            if (this.tus == "7")
                this.str5 = "h";
            if (this.tus == "8")
                this.str5 = "j";
            if (this.tus == "9")
                this.str5 = "k";
            if (this.tus == "0")
                this.str5 = "l";
            if (!(this.tus == "-"))
                return;
            this.str5 = "-";
        }

        public void g3En()
        {
            if (this.tus == "A")
                this.str5 = "1";
            if (this.tus == "B")
                this.str5 = "3";
            if (this.tus == "C")
                this.str5 = "5";
            if (this.tus == "D")
                this.str5 = "7";
            if (this.tus == "E")
                this.str5 = "9";
            if (this.tus == "F")
                this.str5 = "2";
            if (this.tus == "G")
                this.str5 = "4";
            if (this.tus == "H")
                this.str5 = "6";
            if (this.tus == "I")
                this.str5 = "8";
            if (this.tus == "J")
                this.str5 = "0";
            if (this.tus == "K")
                this.str5 = "h";
            if (this.tus == "L")
                this.str5 = "g";
            if (this.tus == "M")
                this.str5 = "f";
            if (this.tus == "N")
                this.str5 = "d";
            if (this.tus == "O")
                this.str5 = "s";
            if (this.tus == "P")
                this.str5 = "a";
            if (this.tus == "Q")
                this.str5 = "p";
            if (this.tus == "R")
                this.str5 = "o";
            if (this.tus == "S")
                this.str5 = "i";
            if (this.tus == "T")
                this.str5 = "u";
            if (this.tus == "U")
                this.str5 = "y";
            if (this.tus == "V")
                this.str5 = "t";
            if (this.tus == "W")
                this.str5 = "r";
            if (this.tus == "X")
                this.str5 = "e";
            if (this.tus == "Y")
                this.str5 = "w";
            if (this.tus == "Z")
                this.str5 = "j";
            if (this.tus == "1")
                this.str5 = "q";
            if (this.tus == "2")
                this.str5 = "k";
            if (this.tus == "3")
                this.str5 = "l";
            if (this.tus == "4")
                this.str5 = "z";
            if (this.tus == "5")
                this.str5 = "x";
            if (this.tus == "6")
                this.str5 = "c";
            if (this.tus == "7")
                this.str5 = "v";
            if (this.tus == "8")
                this.str5 = "b";
            if (this.tus == "9")
                this.str5 = "n";
            if (this.tus == "0")
                this.str5 = "m";
            if (!(this.tus == "-"))
                return;
            this.str5 = "-";
        }

        public void g4En()
        {
            if (this.tus == "A")
                this.str5 = "q";
            if (this.tus == "B")
                this.str5 = "f";
            if (this.tus == "C")
                this.str5 = "h";
            if (this.tus == "D")
                this.str5 = "t";
            if (this.tus == "E")
                this.str5 = "s";
            if (this.tus == "F")
                this.str5 = "i";
            if (this.tus == "G")
                this.str5 = "o";
            if (this.tus == "H")
                this.str5 = "n";
            if (this.tus == "I")
                this.str5 = "5";
            if (this.tus == "J")
                this.str5 = "e";
            if (this.tus == "K")
                this.str5 = "w";
            if (this.tus == "L")
                this.str5 = "c";
            if (this.tus == "M")
                this.str5 = "l";
            if (this.tus == "N")
                this.str5 = "u";
            if (this.tus == "O")
                this.str5 = "g";
            if (this.tus == "P")
                this.str5 = "6";
            if (this.tus == "Q")
                this.str5 = "x";
            if (this.tus == "R")
                this.str5 = "0";
            if (this.tus == "S")
                this.str5 = "m";
            if (this.tus == "T")
                this.str5 = "r";
            if (this.tus == "U")
                this.str5 = "k";
            if (this.tus == "V")
                this.str5 = "7";
            if (this.tus == "W")
                this.str5 = "p";
            if (this.tus == "X")
                this.str5 = "b";
            if (this.tus == "Y")
                this.str5 = "1";
            if (this.tus == "Z")
                this.str5 = "3";
            if (this.tus == "1")
                this.str5 = "j";
            if (this.tus == "2")
                this.str5 = "4";
            if (this.tus == "3")
                this.str5 = "d";
            if (this.tus == "4")
                this.str5 = "2";
            if (this.tus == "5")
                this.str5 = "v";
            if (this.tus == "6")
                this.str5 = "a";
            if (this.tus == "7")
                this.str5 = "9";
            if (this.tus == "8")
                this.str5 = "z";
            if (this.tus == "9")
                this.str5 = "8";
            if (this.tus == "0")
                this.str5 = "y";
            if (!(this.tus == "-"))
                return;
            this.str5 = "-";
        }

        public void g5En()
        {
            if (this.tus == "A")
                this.str5 = "v";
            if (this.tus == "B")
                this.str5 = "p";
            if (this.tus == "C")
                this.str5 = "k";
            if (this.tus == "D")
                this.str5 = "l";
            if (this.tus == "E")
                this.str5 = "y";
            if (this.tus == "F")
                this.str5 = "5";
            if (this.tus == "G")
                this.str5 = "h";
            if (this.tus == "H")
                this.str5 = "6";
            if (this.tus == "I")
                this.str5 = "g";
            if (this.tus == "J")
                this.str5 = "b";
            if (this.tus == "K")
                this.str5 = "7";
            if (this.tus == "L")
                this.str5 = "i";
            if (this.tus == "M")
                this.str5 = "d";
            if (this.tus == "N")
                this.str5 = "o";
            if (this.tus == "O")
                this.str5 = "1";
            if (this.tus == "P")
                this.str5 = "r";
            if (this.tus == "Q")
                this.str5 = "4";
            if (this.tus == "R")
                this.str5 = "u";
            if (this.tus == "S")
                this.str5 = "t";
            if (this.tus == "T")
                this.str5 = "3";
            if (this.tus == "U")
                this.str5 = "a";
            if (this.tus == "V")
                this.str5 = "2";
            if (this.tus == "W")
                this.str5 = "j";
            if (this.tus == "X")
                this.str5 = "0";
            if (this.tus == "Y")
                this.str5 = "n";
            if (this.tus == "Z")
                this.str5 = "c";
            if (this.tus == "1")
                this.str5 = "9";
            if (this.tus == "2")
                this.str5 = "f";
            if (this.tus == "3")
                this.str5 = "8";
            if (this.tus == "4")
                this.str5 = "e";
            if (this.tus == "5")
                this.str5 = "m";
            if (this.tus == "6")
                this.str5 = "q";
            if (this.tus == "7")
                this.str5 = "x";
            if (this.tus == "8")
                this.str5 = "z";
            if (this.tus == "9")
                this.str5 = "w";
            if (this.tus == "0")
                this.str5 = "s";
            if (!(this.tus == "-"))
                return;
            this.str5 = "-";
        }

        public void g6En()
        {
            if (this.tus == "A")
                this.str5 = "l";
            if (this.tus == "B")
                this.str5 = "5";
            if (this.tus == "C")
                this.str5 = "8";
            if (this.tus == "D")
                this.str5 = "m";
            if (this.tus == "E")
                this.str5 = "j";
            if (this.tus == "F")
                this.str5 = "7";
            if (this.tus == "G")
                this.str5 = "6";
            if (this.tus == "H")
                this.str5 = "b";
            if (this.tus == "I")
                this.str5 = "n";
            if (this.tus == "J")
                this.str5 = "4";
            if (this.tus == "K")
                this.str5 = "k";
            if (this.tus == "L")
                this.str5 = "9";
            if (this.tus == "M")
                this.str5 = "0";
            if (this.tus == "N")
                this.str5 = "a";
            if (this.tus == "O")
                this.str5 = "3";
            if (this.tus == "P")
                this.str5 = "2";
            if (this.tus == "Q")
                this.str5 = "f";
            if (this.tus == "R")
                this.str5 = "u";
            if (this.tus == "S")
                this.str5 = "r";
            if (this.tus == "T")
                this.str5 = "1";
            if (this.tus == "U")
                this.str5 = "c";
            if (this.tus == "V")
                this.str5 = "x";
            if (this.tus == "W")
                this.str5 = "e";
            if (this.tus == "X")
                this.str5 = "q";
            if (this.tus == "Y")
                this.str5 = "z";
            if (this.tus == "Z")
                this.str5 = "i";
            if (this.tus == "1")
                this.str5 = "w";
            if (this.tus == "2")
                this.str5 = "v";
            if (this.tus == "3")
                this.str5 = "s";
            if (this.tus == "4")
                this.str5 = "y";
            if (this.tus == "5")
                this.str5 = "p";
            if (this.tus == "6")
                this.str5 = "g";
            if (this.tus == "7")
                this.str5 = "t";
            if (this.tus == "8")
                this.str5 = "h";
            if (this.tus == "9")
                this.str5 = "o";
            if (this.tus == "0")
                this.str5 = "d";
            if (!(this.tus == "-"))
                return;
            this.str5 = "-";
        }
    }
}
