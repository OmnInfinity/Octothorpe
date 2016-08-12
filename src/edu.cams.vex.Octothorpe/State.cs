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
  class State :
        Treelike<State> {
    string name;
    public string Name {
      get {
        return this.name;
      }
      set {
        this.name = value;
      }
    }

    List<Tuple<State, Rule>> connections;
    public List<Tuple<State, Rule>> Connections {
      get {
        return this.connections;
      }
      set {
        this.connections = value;
      }
    }

    bool enterable;
    public bool Enterable {
      get {
        return this.enterable;
      }
      set {
        this.enterable = value;
      }
    }

    bool exitable;
    public bool Exitable {
      get {
        return this.exitable;
      }
      set {
        this.exitable = value;
      }
    }

    public State(string name, int priority = 0) {
      this.name = name;
    }

    public State Start() {
      this.enterable = true;
      return this;
    }

    public State End() {
      this.exitable = true;
      return this;
    }

    public State Connect(State output, Rule rule) {
      // If this is not a final accept state
      if (!this.exitable) {
        this.connections.Add(new Tuple<State, Rule>(output, rule));
      }
      return this;
    }

    public override string ToString() {
      string output = this.name;
      // Start with roots
      if (this.enterable) {
        foreach (Tuple<State, Rule> connection in this.connections) {
          output += " -> ";
          // Recurse, but allow non-roots
          if (!connection.Item1.Enterable) {
            connection.Item1.Enterable = true;
            output += connection.Item1.ToString();
            connection.Item1.Enterable = false;
          }
          else {
            output += connection.Item1.ToString();
          }
          output += "\n";
        }
      }
      return output;
    }
  }
}
