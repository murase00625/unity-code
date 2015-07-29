using UnityEngine;
using System.Collections;


// Super Mario style 2D camera following script.
// Attach this to the primary camera used for rendering the scene.
// Camera SHOULD be orthographic! You'll probably need to tweak the camera settings if it's in perspective.
// The target is the player character object, and the background is the in-scene sprite
//  object that bounds the camera's viewport (i.e. the screen).

// Known issue: If the screen starts at the left edge or right edge of the background,
//  and the camera is the view might glitch for a split second as the player first moves toward the middle.
//  Possibly a Unity camera bug in 4.7f that is fixed in a later version.
// Workaround: Position the camera right on top of the player object.
public class PlatformCameraFollow : MonoBehaviour {
 
        public Transform target;
        public SpriteRenderer background;
        
        // The bounding box for the camera
        private float maxX, maxY, minX, minY;
        private float targetX, targetY;
       
        void Start () {
                Bounds bounds = background.renderer.bounds;
                float cameraHalfHeight = camera.orthographicSize;
                float cameraHalfWidth = cameraHalfHeight * Screen.width / Screen.height;
 
                minX = bounds.min.x + cameraHalfWidth;
                minY = bounds.min.y + cameraHalfHeight;
                maxX = bounds.max.x - cameraHalfWidth;
                maxY = bounds.max.y - cameraHalfHeight;
        }
 
        // Update is called once per frame
        void Update () {
                targetX = target.transform.position.x;
                targetY = target.transform.position.y;
        }
 
        void LateUpdate() {
                Vector3 curPosition = transform.position;
 
                if (targetX > minX && targetX < maxX) {
                        curPosition.x = targetX;
                } else {
                        curPosition.x = Mathf.Clamp(curPosition.x, minX, maxX);
                }
                if (targetY > minY && targetY < maxY) {
                        curPosition.y = targetY;
                } else {
                        curPosition.y = Mathf.Clamp(curPosition.y, minY, maxY);
                }
 
                transform.position = curPosition;
        }
}