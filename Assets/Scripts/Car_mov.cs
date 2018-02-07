using UnityEngine;
using System.Collections;


public class Car_mov : MonoBehaviour
{
  
    WheelJoint2D[] wheelJoints;
    JointMotor2D motor_roda_t;
    float acel_actual = 0f;
    float torqueDir = 0f;
    float vel_max = -5000f;
    float vel_max_back = 2000f;
    float acel_inst = 500;
    float forca_travao = 2500f;
    float grav = 9.81f;
    float ang = 0;



    //rodas
    public Transform rearWheel;
    public Transform frontWheel;

    public float rotation = 0f;
    public float rotation_Speed = 1200f;

    public Rigidbody2D rb;
   
    void Start()
    {

    
        //vou buscar todas as wheeljoits
        wheelJoints = gameObject.GetComponents<WheelJoint2D>();
        // Apenas tenho o motor ligado a toda da frente // 0 pois é o primeiro que defini no body la fora
        motor_roda_t = wheelJoints[0].motor;
    }

 
    void FixedUpdate()
    {


        //angulo do carro
        ang = transform.localEulerAngles.z;

        // para que as contas batam o ang tem de estar entre -180 e 180
        if (ang >= 180) ang = ang - 360;
        acel_actual = Input.GetAxis("Horizontal");
        rotation = Input.GetAxisRaw("Horizontal");

        
        // Se nao estou a acelarar mas o carro ou anda para tras ou esta inclinado para tras 
        if ((acel_actual == 0 && motor_roda_t.motorSpeed < 0) || (acel_actual == 0 && motor_roda_t.motorSpeed == 0 && ang < 0))
        {
           // O clamp faz com o o resultado nao ultrapasse os limites definidos nos 2 e 3 termos da funcao
            motor_roda_t.motorSpeed = Mathf.Clamp(motor_roda_t.motorSpeed - (-acel_inst*0.2f - grav * Mathf.Sin((ang * Mathf.PI) / 180)*90) * Time.deltaTime, vel_max, 0);
        }
        // Se nao estou a acelarar mas o carro ou anda para a frente  ou esta inclinado para a frente  
        else if ((acel_actual == 0 && motor_roda_t.motorSpeed > 0) || (acel_actual == 0 && motor_roda_t.motorSpeed == 0 && ang > 0))
        {
            //decelerate the car while adding the speed if the car is on an inclined plane
            motor_roda_t.motorSpeed = Mathf.Clamp(motor_roda_t.motorSpeed - (acel_inst*0.2f - grav * Mathf.Sin((ang * Mathf.PI) / 180)*90) * Time.deltaTime, 0, vel_max_back);
        }

        
        if (acel_actual != 0)  motor_roda_t.motorSpeed = Mathf.Clamp(motor_roda_t.motorSpeed - (acel_actual * acel_inst - grav * Mathf.Sin((ang * Mathf.PI) / 180)*90) * Time.deltaTime, vel_max, vel_max_back);



        //travao 
        if (Input.GetKey(KeyCode.Space) && motor_roda_t.motorSpeed > 0)
        {
            motor_roda_t.motorSpeed = Mathf.Clamp(motor_roda_t.motorSpeed - forca_travao * Time.deltaTime, 0, vel_max_back);
        }
        
        // dou a velocidade a roda 
        wheelJoints[0].motor = motor_roda_t;

        rb.AddTorque(-rotation * rotation_Speed * Time.deltaTime);

    }



}