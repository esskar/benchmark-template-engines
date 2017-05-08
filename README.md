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
Handlebars.Core|0.52248ms|25000|0.51292ms|25000|0.00020ms|25000
HandlebarsDotNet|0.55260ms|25000|0.53148ms|25000|0.00016ms|25000
RazorLight|9.02100ms|1000|7.86500ms|1000|0.01300ms|1000


##### HelloWorld with Data
Engine|Render|Iterations|Compile|Iterations|Compile&Render|Iterations
------|------|----------|-------|----------|--------------|----------
Handlebars.Core|0.76248ms|25000|0.75024ms|25000|0.00232ms|25000
HandlebarsDotNet|0.80836ms|25000|0.78236ms|25000|0.00220ms|25000
RazorLight|33.50800ms|1000|8.10700ms|1000|0.06400ms|1000


