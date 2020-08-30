using System;
using System.Collections.Generic;
using System.Linq;

namespace VN_Paragraph_Formatter
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = @"
Dear Takuru,
Thank you for the reply. I am glad to see that our letters have reached you safely.
As for us, we are in good health. About that okota we were ruminating about, we bought it in the end yesterday.  It really has been such a long time since we bought furniture for common use that both Yuuto and Uki couldn’t stop going on about it while we were choosing one. However, compared to the two of them, the one who was most excited about it might have been me in fact.
With that, we also rearranged the living room by a bit. You remember that small table in front of the sofa, the one with a dent on the tabletop? We moved that into the storeroom and placed the okota there instead. Also, we moved the sofa to the opposite side. I was wondering if it might perhaps make the place more cramped, but no one seems to have complained over the last two days.
We are also eating our dinner over there nowadays. Sometimes, Arimura and Kazuki would come over to hang out, and they would always refuse to leave the okota when it was time for them to leave. It took a lot of effort just to pull them out. They even went on about getting some legless chairs for themselves and I somehow managed to stop them from doing so.
";

            var paragraphs = toParagraphs(text, 100);
        }

        static IEnumerable<string> toParagraphs(string text, int maxLength)
        {
            int lastFullstop = 0;
            List<string> paragraphs = new List<string>();

            while (lastFullstop < text.Length - 1)
            {
                //each paragraph
                string nextParagraph = string.Empty;

                for (int i = 0; i < maxLength; i++)
                {
                    //each char in paragraph
                    
                    if ((lastFullstop + i) > text.Length - 1)
                    {
                        lastFullstop += i;
                    }
                    else if (text[lastFullstop + i] == '.')
                    {
                        int sentenceLength = (lastFullstop + i) - lastFullstop;
                        nextParagraph += text.Substring(lastFullstop + 1, sentenceLength);

                        lastFullstop += i;
                    }
                }

                if (nextParagraph.Equals("."))
                { 
                    //pass
                }
                else if (nextParagraph.Equals(string.Empty))
                {
                    string firstPart = string.Join(" ", text.Substring(lastFullstop + 1, maxLength).Split(" ").SkipLast(1));
                    string lastPart = string.Join("", text.Substring(lastFullstop + 1 + firstPart.Length).TakeWhile(c => !c.Equals('.')));

                    paragraphs.Add(firstPart.Trim(new char[] { '.', ' ', '\n', '\r' }) + " ");
                    paragraphs.Add(lastPart.Trim(new char[] { '.', ' ', '\n', '\r' }) + ".");

                    lastFullstop += (firstPart + lastPart).Length;
                }
                else
                    paragraphs.Add(nextParagraph.Trim(new char[] { '.', ' ', '\n', '\r' }) + ".");
            }

            return paragraphs;
        }
    }
}
