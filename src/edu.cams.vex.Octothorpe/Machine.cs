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
   * The machine (automaton) for manipulating states
   * 
   * @author Kevin Pho
   * @alias  Automaton
   */
  public class Machine {
    /*
     * The name of the machine for identification or aesthetics
     * 
     * @author Kevin Pho
     */
    string name;
    public string Name {
      get {
        return this.name;
      }
      set {
        this.name = value;
      }
    }

    /*
     * The states to be manipulated as data
     * 
     * @author Kevin Pho
     */
    Table states;
    public Table States {
      get {
        return this.states;
      }
      set {
        this.states = value;
      }
    }

    /*
     * The starting states to be run in a queuelike manner
     * 
     * @author Kevin Pho
     */
    List<string> queue;
    public List<string> Queue {
      get {
        return this.queue;
      }
      set {
        this.queue = value;
      }
    }
    
    /*
     * Constructs the machine
     * 
     * @author    Kevin Pho
     * @parameter name (string) for naming the machine
     * @parameter Table (states) for pre-loading
     * @return    this (Machine) for an object instance
     */
    public Machine(string name, Table states) {
      this.name = name;
      this.states = states;
      this.queue = new List<string>();

      // Add each state to the queue, if it is a start state
      foreach (State state in states.Values.ToList()) {
        if (state.Enterable) {
          this.queue.Add(state.Name);
        }
      }
    }

    /* Todo */
    public void Traverse() { }

    /* Todo */
    public void Accepts() { }

    /*
     * Go through a single state and print
     * 
     * Recursively print out the machine.
     * 
     * @author    Kevin Pho
     * @parameter state (State) for traversal
     * @parameter level (int) for recording the current position
     * @return    this (Machine) for an object instance
     */
    public string Trace(State state, int level, int length = 10) {
      string output = "";

      // Space out the current state and add its gridded name
      output += " ".Repeat(length).Repeat(level);
      output += state.Name.Grid(length);

      /* Todo: Traverse and return non-cyclic tree */

      // Run through the connections
      int number = 0;
      /* Todo: Update state.Connections to new tree */
      foreach (Tuple<Cause, State, Effect> connection in state.Connections) {
        // Space out to correct depth after the first one, which is on the same line
        if (number != 0) {
          output += " ".Repeat(length).Repeat(level + 1);
        }

        // Print an arrow
        output += " "; /* Spacer */
        output += "-".Repeat((int) Math.Ceiling((double) length/2) - 3); /* Half of it plus space for the other symbols*/
        output += "> "; /* Arrow and spacer */

        // Add the name
        output += connection.Item2.Name.Grid(length);

        // If there are more, add a newline
        if (number < state.Connections.Count - 1) {
          output += "\n";
        }
        number++;
      }

      /* Todo: Draw transitions */
      return output;
    }

    /*
     * Go through a single state and print
     * 
     * Override the original.
     * 
     * @author    Kevin Pho
     * @return    string for an interpretation of the tree
     */
    public override string ToString() {
      string output = "";

      // Run through the start states
      int number = 0;
      foreach (string start in this.queue) {
        // Run through the state as a root
        output += Trace(this.states[start], 0);

        // If there are more, add a newline
        if (number < this.queue.Count - 1) {
          output += "\n";
        }
        number++;
      }
      return output;
    }
  }
}
