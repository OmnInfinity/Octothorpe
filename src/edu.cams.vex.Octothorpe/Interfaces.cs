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
   * A species that connects to the same species
   * 
   * The connections can be chained. The tree can be connected to other trees, acting as branches.
   * 
   * @author Kevin Pho
   * @alias  Connectable
   */
  public interface Treelike<T> {
    /*
     * Add an identical species with a rule
     * 
     * A specific type must be specified as the first generic.
     * 
     * @author    Kevin Pho
     * @parameter choice (Cause) for controlling a state traversal
     * @parameter output (T) for adding an output species
     * @parameter result (Effect) for following up a state traversal
     * @return    T as the evaluation of the conditional
     */
    T Connect(Cause choice, T output, Effect result);
  }
}
