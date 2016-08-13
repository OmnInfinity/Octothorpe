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
   * The individual states
   * 
   * @author Kevin Pho
   * @base   Treelike<State> due to branching connections
   */
  class State :
                Treelike<State> {
    /* The name of the state for reference and unique identification
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
    
    /* The priority of accepting this state
     * 
     * @author Kevin Pho
     */
    int priority;
    public int Priority {
      get {
        return this.priority;
      }
      set {
        this.priority = value;
      }
    }

    /* The output connections
     * 
     * @author Kevin Pho
     */
    List<Tuple<Cause, State, Effect>> connections;
    public List<Tuple<Cause, State, Effect>> Connections {
      get {
        return this.connections;
      }
      set {
        this.connections = value;
      }
    }

    /* Whether the state is an initial state
     * 
     * @author Kevin Pho
     */
    bool enterable;
    public bool Enterable {
      get {
        return this.enterable;
      }
      set {
        this.enterable = value;
      }
    }

    /* Whether the state is a final state
     * 
     * @author Kevin Pho
     */
    bool exitable;
    public bool Exitable {
      get {
        return this.exitable;
      }
      set {
        this.exitable = value;
      }
    }

    /* Constructs the state
     * 
     * @author    Kevin Pho
     * @parameter name (string) for naming the state
     * @parameter priority (int) for solving ambiguity in accept states
     * @return    this (State) for an object instance
     */
    public State(string name, int priority = 0) {
      this.name = name;
      this.priority = priority;
      this.connections = new List<Tuple<Cause, State, Effect>>();
      this.enterable = false;
      this.exitable = false;
    }
    
    /* Makes the state an initial state
     * 
     * @author Kevin Pho
     * @return this (State) for chaining
     */
    public State Start() {
      this.enterable = true;
      return this;
    }

    /* Makes the state a final state
     * 
     * @author Kevin Pho
     * @return this (State) for chaining
     */
    public State End() {
      this.exitable = true;
      return this;
    }

    /* Connects another state
     * 
     * @author    Kevin Pho
     * @parameter choice (Cause) for deciding to connect
     * @parameter output (State) for adding the state
     * @parameter result (Effect) for doing something
     * @return    this (State) for chaining
     */
    public State Connect(Cause choice, State output, Effect result) {
      this.connections.Add(new Tuple<Cause, State, Effect>(choice, output, result));
      return this;
    }
  }
}
