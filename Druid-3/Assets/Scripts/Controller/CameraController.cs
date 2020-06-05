using Interface;
using Model;
using UnityEngine;


namespace Controller
{
    public sealed class CameraController : BaseController, IExecute
    {
        #region Fields

        private CameraModel _cameraModel;

        #endregion

        
        #region Properties

        private CameraModel BackCamera
        {
            get
            {
                if (!_cameraModel)
                    _cameraModel = Object.FindObjectOfType<CameraModel>();
                return _cameraModel;
            }
        }

        #endregion

        
        #region Methods

        public void ZoomCamera(float deltaAxis)
        {
            if (deltaAxis > 0) BackCamera.AddOffsetZ();
            else if (deltaAxis < 0) BackCamera.RemoveOffsetZ();

            BackCamera.ClampByZ();
        }

        public void RotateCamera(float axisX, float axisY)
        {
            BackCamera.AxisX = axisX;
            BackCamera.AxisY = axisY;

            BackCamera.ClampByY();

            BackCamera.Rotate();
        }

        public void Execute()
        {
            BackCamera.CheckVisibilityTargetAndMoveCamera();
        }

        #endregion
    }
}