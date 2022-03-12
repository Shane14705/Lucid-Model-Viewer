using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class SceneStateModel
{
    //Holds annotations and their locations on the model -- whenever a new annotation is created, it is added to this list.
    //When an annotation is removed or added, the SceneStateComponent will add/remove it to the scene
    [RealtimeProperty(1, true, true)]
    private RealtimeDictionary<AnnotationModel> _annotations;
    
}

/* ----- Begin Normal Autogenerated Code ----- */
public partial class SceneStateModel : RealtimeModel {
    public Normal.Realtime.Serialization.RealtimeDictionary<AnnotationModel> annotations {
        get => _annotations;
    }
    
    public enum PropertyID : uint {
        Annotations = 1,
    }
    
    #region Properties
    
    private ModelProperty<Normal.Realtime.Serialization.RealtimeDictionary<AnnotationModel>> _annotationsProperty;
    
    #endregion
    
    public SceneStateModel() : base(null) {
        RealtimeModel[] childModels = new RealtimeModel[1];
        
        _annotations = new Normal.Realtime.Serialization.RealtimeDictionary<AnnotationModel>();
        childModels[0] = _annotations;
        
        SetChildren(childModels);
        
        _annotationsProperty = new ModelProperty<Normal.Realtime.Serialization.RealtimeDictionary<AnnotationModel>>(1, _annotations);
    }
    
    protected override int WriteLength(StreamContext context) {
        var length = 0;
        length += _annotationsProperty.WriteLength(context);
        return length;
    }
    
    protected override void Write(WriteStream stream, StreamContext context) {
        var writes = false;
        writes |= _annotationsProperty.Write(stream, context);
        if (writes) InvalidateContextLength(context);
    }
    
    protected override void Read(ReadStream stream, StreamContext context) {
        var anyPropertiesChanged = false;
        while (stream.ReadNextPropertyID(out uint propertyID)) {
            var changed = false;
            switch (propertyID) {
                case (uint) PropertyID.Annotations: {
                    changed = _annotationsProperty.Read(stream, context);
                    break;
                }
                default: {
                    stream.SkipProperty();
                    break;
                }
            }
            anyPropertiesChanged |= changed;
        }
        if (anyPropertiesChanged) {
            UpdateBackingFields();
        }
    }
    
    private void UpdateBackingFields() {
        _annotations = annotations;
    }
    
}
/* ----- End Normal Autogenerated Code ----- */
