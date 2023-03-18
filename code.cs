using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
  class Dictionary
  {
    List<String> WordList = new List<String>();

    public Dictionary()
    {
      WordList.Add("this");
      WordList.Add("is");
      WordList.Add("Wednesday");
      WordList.Add("my");
      WordList.Add("dudes");
    }

    public List<String> Converter(string Text)
    {
      List<string> TextList = new List<string>();
      int TextLength = Text.Length;

      string Word = "";
      for (int LetterIndex = 0; LetterIndex < TextLength; ++LetterIndex)
      {
        if (Text[LetterIndex] == ' ')
        {
          TextList.Add(Word);
          Word = "";
        }
        else
        {
          Word += Text[LetterIndex];
        }
      }
      return TextList;
    }

    public string PhoneNumberCorrector(string Text)
    {
      int TextLength = Text.Length;
      string NewText = "";
      for (int LetterIndex = 0; LetterIndex < TextLength - 4; ++LetterIndex)
      {
        if(Text[LetterIndex] == '(' && Text[LetterIndex + 4] == ')')
        {
          NewText += "+7 ";

          if(Text[LetterIndex + 1] != '0')
          {
            NewText += Text[LetterIndex + 1];
          }
          if(Text[LetterIndex + 1] != '0' || Text[LetterIndex + 2] != '0')
          {
            NewText += Text[LetterIndex + 2];
          }
          NewText += Text[LetterIndex + 3] + " ";

          for(int LetterNumberIndex = LetterIndex + 6; LetterNumberIndex < LetterNumberIndex + 14 && LetterNumberIndex != TextLength; ++LetterNumberIndex)
          {
            if(Text[LetterNumberIndex] == '-')
            {
              NewText += ' ';
            }
            else
            {
              NewText += Text[LetterNumberIndex];
            }
          }
          LetterIndex += 14;
        }
        else
        {
          NewText += Text[LetterIndex];
        }
      }
      return NewText;
    }

    public string Corrector(string Word)
    {
      int WordLength = Word.Length;
      string NewText = "";
      for (int SuspiciousLetter = 0; SuspiciousLetter < WordLength; ++SuspiciousLetter)
      {
        for (int LetterIndex = 0; LetterIndex < WordLength; ++LetterIndex)
        {
          if(LetterIndex == SuspiciousLetter && LetterIndex < WordLength - 1)
          {
            NewText += Word[LetterIndex + 1];
            NewText += Word[LetterIndex];
            ++LetterIndex;
          }
          else
          {
            NewText += Word[LetterIndex];
          }
        }
        if (WordList.Contains(NewText))
        {
          return WordList[WordList.IndexOf(NewText)];
        }
        NewText = "";
      }
      return Word;
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      StreamReader sr = new StreamReader("text.txt");
      string Text = sr.ReadLine();

      Console.WriteLine("текст файла:");
      Console.WriteLine(Text);

      Dictionary dictionary = new Dictionary();
  
      List<string> TextList = dictionary.Converter(Text);
      string NewText = "";

      for (int WordIndex = 0; WordIndex < TextList.Count; ++WordIndex)
      {
        TextList[WordIndex] = dictionary.Corrector(TextList[WordIndex]);
        NewText += (TextList[WordIndex] + " ");
      }
 
      NewText = dictionary.PhoneNumberCorrector(NewText);

      Console.WriteLine("отредактированный файл:");
      Console.WriteLine(NewText);

      Console.ReadKey();
    }
  }
}
