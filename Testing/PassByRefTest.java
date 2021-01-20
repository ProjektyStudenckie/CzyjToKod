class PassByRefTest {

    static class Ref<T> {
        T val;
        Ref(T val) { this.val = val; }
    }
    
    static void changeMe(Ref<String> s) {
        s.val = "Changed";
    }

    static void swap(Ref<Integer> x, Ref<Integer> y) {
        int temp = x.val;

        x.val = y.val;
        y.val = temp;
    }

    public static void main(String[] args) {
        var a = new Ref(5);
        var b = new Ref(10);
        var s = new Ref("still unchanged");
        
        swap(a, b);
        changeMe(s);

        System.out.println( "a = " + a.val + ", " +
                            "b = " + b.val + ", " +
                            "s = " + s.val );
    }
}