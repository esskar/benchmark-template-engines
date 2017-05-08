# benchmark-template-engines
Trying to benchmark different .NET based template engines.

## Engines
Here is a complete list of all engines that are currently benchmarked

* [Handlebars.Core](https://github.com/esskar/handlebars-core)
* [HandlebarsDotNet](https://github.com/rexm/Handlebars.Net)

## Results
All benchmark tests are automated and based on 25000 iterations.

#### In detail

##### HelloWorld
Engine|Render|Compile|Compile&Render
------|------|-------|--------------
Handlebars.Core|0.5232ms|0.5196ms|0.00016ms
HandlebarsDotNet|0.56264ms|0.53708ms|0.00016ms


##### HelloWorld with Data
Engine|Render|Compile|Compile&Render
------|------|-------|--------------
Handlebars.Core|0.78044ms|0.76492ms|0.00228ms
HandlebarsDotNet|0.81152ms|0.77364ms|0.00232ms


