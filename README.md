# benchmark-template-engines
Trying to benchmark different .NET based template engines.

## Engines
Here is a complete list of all engines that are currently benchmarked

* [Handlebars.Core](https://github.com/esskar/handlebars-core)
* [HandlebarsDotNet](https://github.com/rexm/Handlebars.Net)
* [RazorLight](https://github.com/toddams/RazorLight)

## Results
All benchmark tests are automated.

### Average
Engine|Render|Iterations|Compile|Iterations|Compile&Render|Iterations
------|------|----------|-------|----------|--------------|----------
Handlebars.Core|0.59654ms|25000|0.58818ms|25000|0.00118ms|25000
HandlebarsDotNet|0.66506ms|25000|0.64892ms|25000|0.00124ms|25000
RazorLight|16.79700ms|1000|6.92250ms|1000|0.03200ms|1000


### In detail
#### HelloWorld
Engine|Render|Iterations|Compile|Iterations|Compile&Render|Iterations
------|------|----------|-------|----------|--------------|----------
Handlebars.Core|0.48616ms|25000|0.47632ms|25000|0.00016ms|25000
HandlebarsDotNet|0.54484ms|25000|0.52888ms|25000|0.00016ms|25000
RazorLight|7.35200ms|1000|6.55400ms|1000|0.01100ms|1000

#### HelloWorld with Data
Engine|Render|Iterations|Compile|Iterations|Compile&Render|Iterations
------|------|----------|-------|----------|--------------|----------
Handlebars.Core|0.70692ms|25000|0.70004ms|25000|0.00220ms|25000
HandlebarsDotNet|0.78528ms|25000|0.76896ms|25000|0.00232ms|25000
RazorLight|26.24200ms|1000|7.29100ms|1000|0.05300ms|1000


