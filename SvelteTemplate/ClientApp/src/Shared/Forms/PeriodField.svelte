<script>
    import moment from 'moment'
    export let text = "Поле"
    export let required = false;
    export let className = "form-control"
    export let value = null;

    $: innerValue = value? moment(value, 'YYYYMM').format('YYYY-MM'): null
    

    const change = (e) => {
        if (!e.target.value) value = null;
        else value = moment(e.target.value).format('YYYYMM')
    }
</script>


<label for="input{text}">{text}:</label>
<input
  {required}
  value = {innerValue}
  on:change={change}
  type="month"
  class={className}
  id="input{text}" 
  />
