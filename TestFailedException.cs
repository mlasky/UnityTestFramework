using System;

public class TestFailedException : Exception {
    public string message;

    public TestFailedException(string msg) : base(msg) {
        message = msg;
    }
}