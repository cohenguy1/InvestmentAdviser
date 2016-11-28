update VectorsAssignments
set NextAskHeuristic = 'Optimal'
where NextAskHeuristic = 'Last'

update VectorsAssignments
set LastStarted = null