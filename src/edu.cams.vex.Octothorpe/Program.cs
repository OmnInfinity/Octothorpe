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
  class Program {
    // Execution codes
    public static int Success = 0;

    static int Main(string[] args) {
      /* And then, there was light */
      Console.WriteLine("Octothorpe by OmnInfinity");

      /* A world had been made; from creation to destruction */
      // Tokenizer for lexical analysis
      Graph<State> States = new Graph<State> {
        { "q0", new State("q0").Start().End() },
        { "q1", new State("q1")               },
        { "q2", new State("q2")        .End() },
      };

      Rules<State> Transitions = new Rules<State>() {
        { new Transition("q0", "a", "q1") },
        { new Transition("q1", "a", "q2") },
        { new Transition("q2", "a", "q1") },
      };

      Transitions.Establish(ref States);

      States["q0"].Connect(States["q1"], delegate(string input) { return true; });
      States["q0"].ToString();

      Graph<State> Machine = States;
      Machine.Accepts("");
      Machine.Accepts("a");
      Machine.Accepts("aa");

      /* And thus, there was void */
      Console.ReadKey();
      return Program.Success;
    }
  }
}
