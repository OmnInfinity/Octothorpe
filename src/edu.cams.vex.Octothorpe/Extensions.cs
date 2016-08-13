/*
 * MIT License
 * 
 * Copyright (c) 2016 Kevin Pho (OmnInfinity) <kevinkhoapho@gmail.com>,
 *                    CAMS VEX 687 "Nerd Herd",
 *                    CAMS VEX 687-E
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System; // For input/output
using System.Collections.Generic; // For collections
using System.Linq; // For selection
using System.Text; // For text manipulation
using System.Threading.Tasks; // For multitasking

using Octothorpe = edu.cams.vex.Octothorpe;

namespace edu.cams.vex.Octothorpe {
  /*
   * Container of extensions
   * 
   * @author Kevin Pho
   */
  static class Extensions {

    /*
     * Repeat a string
     * 
     * @author    Kevin Pho
     * @parameter input (string) for repeating
     * @parameter multiples (int) for number of repeats
     * @return    string for a result
     */
    public static string Repeat(this string input, int multiples) {
      string output = "";

      // Repeat the string
      for (int counter = 0; counter < multiples; counter++) {
        output += input;
      }

      return output;
    }

    /*
     * Pad a string
     * 
     * @author    Kevin Pho
     * @parameter input (string) for padding
     * @parameter length (int) for size of the result
     * @parameter fill (string) for filling the spaces
     *  @default  " "
     * @return    string for a result
     */
    public static string Pad(this string input, int length, string fill = " ") {
      string output = "";

      /* Remove this to pad to nearest multiple */
      // If the length of the input is bigger than the target length, stop
      if (input.Length > length) {
        return input;
      }

      // If the fill is empty (zero-length), stop
      if (fill == "") {
        return input;
      }

      // Remaining characters left
      int space = Math.Abs(length) - (input.Length % length);

      // Minimum number of fills
      int fills = (int)Math.Ceiling((double)space / fill.Length);

      // Align by direction
      if (Math.Sign(length) == -1) { /* Left align */
        // Append truncated filler
        output = input + Repeat(fill, fills).Substring(0, space);
      }
      else { /* Right align */
        output = Repeat(fill, fills).Substring(0, space) + input;
      }

      return output;
    }

    /*
     * Overflow a string
     * 
     * @author    Kevin Pho
     * @parameter input (string) for overflowing
     * @parameter length (int) for size of the result
     * @parameter gap (string) for cutting out
     *  @default  "..."
     * @return    string for a result
     */
    public static string Overflow(this string input, int length, string gap = "...") {
      string output = "";

      // Truncate the fill itself to fit within the block
      if (gap.Length > Math.Abs(length)) {
        gap = gap.Substring(0, length);
      }

      // If there are trailing characters
      if (input.Length > length) {
        // Align by direction
        if (Math.Sign(length) == -1) { /* Left align */
          output = input.Substring(0, Math.Abs(length) - gap.Length) + gap;
        }
        else { /* Right align */
          output = gap + input.Substring(0, Math.Abs(length) - gap.Length);
        }
      }
      return output;
    }

    /*
     * Fit a string to a grid
     * 
     * @author    Kevin Pho
     * @parameter input (string) for overflowing
     * @parameter length (int) for size of the result
     * @parameter gap (string) for cutting out
     *  @default  "..."
     * @return    string for a result
     */
    public static string Grid(this string input, int length, string fill = " ", string gap = "...") {
      string output = "";
      if (input.Length < length) {
        output = Pad(input, -length, fill);
      }
      else if (input.Length == length) {
        output = input;
      }
      else {
        output = Overflow(input, -length, gap);
      }
      return output;
    }
  }
}
