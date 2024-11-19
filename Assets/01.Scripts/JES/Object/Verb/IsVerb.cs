public class IsVerb : Verb
{
    public override void ApplyVerb(Subject subject, IVerbable verbable)
    {
        verbable.VerbApply(subject.GetAgents());
    }
}