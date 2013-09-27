using System;

public class TestFailedException : Exception {
    public string testStatus;

    public TestFailedException(string msg) : base(msg) {
        testStatus = msg;
    }
}