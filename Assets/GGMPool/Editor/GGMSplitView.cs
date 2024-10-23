using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GGMSplitView : TwoPaneSplitView
{

    public new class UxmlFactory : UxmlFactory<GGMSplitView,UxmlTraits> { }
    public new class UxmlTraits : TwoPaneSplitView.UxmlTraits { }

}
