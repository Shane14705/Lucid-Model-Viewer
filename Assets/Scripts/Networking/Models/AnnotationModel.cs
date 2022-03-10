using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;


/*
 * Model responsible for storing everything required to synchronize an annotation over the network.
 */

[RealtimeModel]
public partial class AnnotationModel
{
   
}

/* ----- Begin Normal Autogenerated Code ----- */
public partial class AnnotationModel : RealtimeModel {
    public AnnotationModel() : base(null) {
    }
    
    protected override int WriteLength(StreamContext context) {
        return 0;
    }
    protected override void Write(WriteStream stream, StreamContext context) {
    }
    protected override void Read(ReadStream stream, StreamContext context) {
    }
    private void UpdateBackingFields() {
    }
    
}
/* ----- End Normal Autogenerated Code ----- */