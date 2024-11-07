public class IsVerb : Verb
{
    protected override void ApplyVerb(Subject subject, IVerbable verbable)
    {
        verbable.VerbApply(subject.GetAgents());
    }
}