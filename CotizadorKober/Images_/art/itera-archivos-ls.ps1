
#$a = ls -n 950*
#
#foreach ($i in $a)
#{
#	$x = $i
#	ren $i ("TEKA " + $x)
#	ls $i
#}

$a = ls -n $args[0]
$cnt = 0

foreach ($i in $a)
{
	$x = $i
	ren $i ($args[1] + $x)
	$cnt ++
}

echo "Se modificaron " $cnt " archivos."
