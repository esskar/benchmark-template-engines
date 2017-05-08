# benchmark-template-engines
Trying to benchmark different .NET based template engines.

## Engines
Here is a complete list of all engines that are currently benchmarked

* [Handlebars.Core](https://github.com/esskar/handlebars-core)
* [HandlebarsDotNet](https://github.com/rexm/Handlebars.Net)
* [RazorLight](https://github.com/toddams/RazorLight)

## Results
All benchmark tests are automated.

#### In detail

##### HelloWorld
Engine|Render|Iterations|Compile|Iterations|Compile&Render|Iterations
------|------|----------|-------|----------|--------------|----------
Handlebars.Core|0.48472ms|25000|0.48280ms|25000|0.00020ms|25000
HandlebarsDotNet|0.52556ms|25000|0.49544ms|25000|0.00016ms|25000
RazorLight|65.00000ms|1|11.00000ms|1|0.00000ms|1


##### HelloWorld with Data
Engine|Render|Iterations|Compile|Iterations|Compile&Render|Iterations
------|------|----------|-------|----------|--------------|----------
Handlebars.Core|0.92380ms|25000|1.04944ms|25000|0.00336ms|25000
HandlebarsDotNet|0.75436ms|25000|0.74068ms|25000|0.00204ms|25000
RazorLight|225.00000ms|1|12.00000ms|1|9.00000ms|1


