using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TicTacToe : MonoBehaviour
{
    bool isPlayerX = true; // Đánh dấu lượt của player X
    bool gameOver = false; // Biến kiểm tra trạng thái kết thúc trò chơi
    public Text[] buttons = new Text[100]; // Mảng chứa các nút
    public Text msgFeedBack;
    public Text txtPlayerX;
    public Text txtPlayerO;
    int size = 10; // Kích thước bàn cờ 10x10

    void Start()
    {
        InitializeBoard();
    }

    void InitializeBoard()
    {
        // Gán sự kiện click cho từng nút
        for (int i = 0; i < size * size; i++)
        {
            int index = i; // Lưu lại chỉ số của nút để sử dụng trong closure
            if (buttons[i] != null) // Kiểm tra xem nút có tồn tại không
            {
                buttons[i].GetComponent<Button>().onClick.AddListener(() => OnButtonClick(index));
            }
            else
            {
                Debug.LogError("Button at index " + i + " is not assigned!");
            }
        }
    }

    public void OnButtonClick(int index)
    {
        if (!gameOver && buttons[index].text == "")
        {
            // Đặt X hoặc O vào ô được chọn
            buttons[index].text = isPlayerX ? "X" : "O";
            // Kiểm tra chiến thắng
            if (CheckWin())
            {
                msgFeedBack.text = "The winner is player " + (isPlayerX ? "X" : "O") + "!!!";
                msgFeedBack.color = isPlayerX ? Color.red : Color.blue;
                UpdateScore(isPlayerX);
                gameOver = true; // Đặt trạng thái kết thúc trò chơi thành true
                return;
            }
            // Chuyển lượt đánh cho người chơi tiếp theo
            isPlayerX = !isPlayerX;
        }
    }

    bool CheckWin()
    {
        // Kiểm tra từng nhóm 5 ô liên tiếp
        for (int i = 0; i < size * size; i++)
        {
            // Kiểm tra hàng ngang
            if (i % size <= size - 5)
            {
                bool horizontalWin = true;
                for (int j = 0; j < 5; j++)
                {
                    if (buttons[i + j].text != (isPlayerX ? "X" : "O"))
                    {
                        horizontalWin = false;
                        break;
                    }
                }
                if (horizontalWin) return true;
            }

            // Kiểm tra hàng dọc
            if (i / size <= size - 5)
            {
                bool verticalWin = true;
                for (int j = 0; j < 5; j++)
                {
                    if (buttons[i + j * size].text != (isPlayerX ? "X" : "O"))
                    {
                        verticalWin = false;
                        break;
                    }
                }
                if (verticalWin) return true;
            }

            // Kiểm tra đường chéo chính
            if (i % size <= size - 5 && i / size <= size - 5)
            {
                bool diagonalMainWin = true;
                for (int j = 0; j < 5; j++)
                {
                    if (buttons[i + j * (size + 1)].text != (isPlayerX ? "X" : "O"))
                    {
                        diagonalMainWin = false;
                        break;
                    }
                }
                if (diagonalMainWin) return true;
            }

            // Kiểm tra đường chéo phụ
            if (i % size >= 4 && i / size <= size - 5)
            {
                bool diagonalAntiWin = true;
                for (int j = 0; j < 5; j++)
                {
                    if (buttons[i + j * (size - 1)].text != (isPlayerX ? "X" : "O"))
                    {
                        diagonalAntiWin = false;
                        break;
                    }
                }
                if (diagonalAntiWin) return true;
            }
        }
        return false;
    }

    void UpdateScore(bool isPlayerX)
    {
        if (isPlayerX)
        {
            int score = int.Parse(txtPlayerX.text);
            txtPlayerX.text = (score + 1).ToString();
        }
        else
        {
            int score = int.Parse(txtPlayerO.text);
            txtPlayerO.text = (score + 1).ToString();
        }
    }

    public void btnReset_Click()
    {
        // Reset tất cả các ô trên bàn cờ
        foreach (Text button in buttons)
        {
            if (button != null) // Kiểm tra xem nút có tồn tại không
            {
                button.text = "";
            }
        }
        msgFeedBack.text = "";
        gameOver = false; // Reset trạng thái kết thúc trò chơi
        isPlayerX = true; // Reset lại lượt đánh
    }

    public void btnNewGame_Click()
    {
        // Khởi động một trò chơi mới
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
