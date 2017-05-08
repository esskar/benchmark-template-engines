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
|Render|Compile|Compile&Render
|------|-------|--------------
Handlebars.Core|0.51892ms|0.51428ms|0.00016ms
HandlebarsDotNet|0.55304ms|0.54248ms|0.00016ms


