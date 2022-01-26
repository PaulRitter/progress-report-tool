// See https://aka.ms/new-console-template for more information

using ProgRepTools;

if(args.Length < 2)
{
    Console.WriteLine("Usage: prt <mode> <project> [<optional args>]");
    return -1;
}

var proj = Project.Load(args[1]);
switch (args[0])
{
    case "create":
        proj.Save();
        break;
    case "edit":
        string target = null;
        if (args.Length > 2)
            target = args[2];
        proj.Edit(target);
        proj.Save();
        break;
    case "list":
        if (args.Length != 3)
        {
            Console.WriteLine("Usage: prt list <project> (prs/contribs)");
            return -1;
        }
        //todo
        break;
    case "count":
        if (args.Length != 3)
        {
            Console.WriteLine("Usage: prt count <project> (prs/commits)");
            return -1;
        }
        //todo
        break;
    case "setcheck":
        proj.LastCheck = DateTime.Now;
        proj.Save();
        break;
}