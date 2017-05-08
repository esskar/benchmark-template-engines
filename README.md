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
Handlebars.Core|0.62402ms|25000|0.61918ms|25000|0.00126ms|25000
HandlebarsDotNet|0.66422ms|25000|0.64458ms|25000|0.00116ms|25000
RazorLight|23.67300ms|1000|8.58700ms|1000|0.04350ms|1000


### In detail
#### HelloWorld
Engine|Render|Iterations|Compile|Iterations|Compile&Render|Iterations
------|------|----------|-------|----------|--------------|----------
Handlebars.Core|0.49736ms|25000|0.49884ms|25000|0.00020ms|25000
HandlebarsDotNet|0.54872ms|25000|0.52024ms|25000|0.00016ms|25000
RazorLight|8.87500ms|1000|7.77300ms|1000|0.01500ms|1000

#### HelloWorld with Data
Engine|Render|Iterations|Compile|Iterations|Compile&Render|Iterations
------|------|----------|-------|----------|--------------|----------
Handlebars.Core|0.75068ms|25000|0.73952ms|25000|0.00232ms|25000
HandlebarsDotNet|0.77972ms|25000|0.76892ms|25000|0.00216ms|25000
RazorLight|38.47100ms|1000|9.40100ms|1000|0.07200ms|1000


