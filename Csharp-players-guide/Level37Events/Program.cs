using Level37Events.Challenges;

var tree = new CharberryTree();
var notifier = new Notifier(tree);
var harvester = new Harverster(tree);

while (true)
    tree.MaybeGrow();