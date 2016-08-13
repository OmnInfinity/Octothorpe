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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Octothorpe = edu.cams.vex.Octothorpe;

namespace edu.cams.vex.Octothorpe {
  /*
   * The machine (automaton)
   * 
   * @author Kevin Pho
   */
  class Machine {
    Table states;
    public Table States {
      get {
        return this.states;
      }
      set {
        this.states = value;
      }
    }

    List<string> queue;
    public List<string> Queue {
      get {
        return this.queue;
      }
      set {
        this.queue = value;
      }
    }

    public Machine(Table states) {
      this.states = states;
      this.queue = new List<string>();
      foreach (State state in states.Values.ToList()) {
        if (state.Enterable) {
          this.queue.Add(state.Name);
        }
      }
    }

    public string Repeat(string input, int multiples) {
      string output = "";
      for (int counter = 0; counter < multiples; counter++) {
        output += input;
      }
      return output;
    }

    public string Pad(string input, int length, string fill) {
      string output = "";

      // Remaining characters left
      int space = Math.Abs(length) - (input.Length % length);

      // Minimum number of fills
      int fills = (int) Math.Ceiling((double) space/fill.Length);
      
      // Align by direction
      if (Math.Sign(length) == -1) { /* Left align */
        output = input + Repeat(fill, fills).Substring(0, space);
      }
      else { /* Right align */
        output = Repeat(fill, fills).Substring(0, space) + input;
      }
      return output;
    }

    public string Truncate(string input, int length, string fill) {
      string output = "";
      // Truncate the fill itself
      if (fill.Length > Math.Abs(length)) {
        fill = Truncate(fill, length, "");
      }
      if (input.Length > length) {
        if (Math.Sign(length) == -1) { /* Left align */
          output = input.Substring(0, Math.Abs(length) - fill.Length) + fill;
        }
        else { /* Right align */
          output = fill + input.Substring(0, Math.Abs(length) - fill.Length);
        }
      }
      return output;
    }

    public string Trace(State state, int level, int size = 10) {
      string output = "";
      output += Repeat(Repeat(" ", size), level);

      if (state.Name.Length < size) {
        state.Name = Pad(state.Name, -size, "-");
      }
      else if (state.Name.Length == size) { }
      else {
        state.Name = Truncate(state.Name, -size, "...");
      }
      output += state.Name;

      foreach (Tuple<Cause, State, Effect> connection in state.Connections) {
        output += Repeat(Repeat(" ", size), level);
        output += "a";
      }

      // draw arrows
      // draw children
      // draw transitions
      return output;
    }

    public override string ToString() {
      string output = "";
      int number = 0;
      foreach (string start in this.queue) {
        output += Trace(this.states[start], 0);

        // If there are more
        if (number < this.queue.Count - 1) {
          output += "\n";
        }
        number++;
      }
      return output;
    }
  }
}
