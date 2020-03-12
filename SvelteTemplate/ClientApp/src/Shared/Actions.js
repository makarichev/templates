import moment from 'moment'
import { bind, assign, set_data } from 'svelte/internal'

export function asDate(node, value) {
    return {

        update: function(newValue) {
            node.innerHTML = newValue? moment(newValue).format('DD.MM.YYYY'): ''
        }
    }

}

export function selectTextOnFocus(node) {
  
    const handleFocus = event => {
      node && typeof node.select === 'function' && node.select()
    }
    
    node.addEventListener('focus', handleFocus)
    
    return {
      destroy() {
        node.removeEventListener('focus', handleFocus)
      }
    }
  }

export function bindDate(node) {
    

    node.type = "date";

    //node.value = moment(value).format('YYYY-MM-DD');

    function update (newValue) {
        console.log(newValue);
        value = newValue;
        // value = moment(newValue).format('YYYY-MM-DD');
        // node.value = moment(newValue).format('YYYY-MM-DD')
    };

    function onChange(e) {
        
        //let d = moment(e.target.value).format('YYYY-MM-DDT00:00:00')
        //set_data(d, value)
        //assign(current_component, value, moment(e.target.value).format('YYYY-MM-DDT00:00:00'))
        // let _value = e.target.value
        // //node.value = moment(value).format('YYYY-MM-DDT00:00:00')

        //update(moment(e.target.value).format('YYYY-MM-DDT00:00:00'));
        // //node.value = moment(value).format('YYYY-MM-DDT00:00:00')
        console.log(moment(e.target.value).format('YYYY-MM-DDT00:00:00'))
        
        e.stopPropagation()
        //e.target.value = moment(e.target.value).format('YYYY-MM-DDT00:00:00')
    
    }

    node.addEventListener('input', onChange )

    return {
        destroy: () => {
            node.removeEventListener('mousedown', onChange);
        }
    }




    // if (node.nodeName==='INPUT') {
        
    //     node.type = "date"
        
    //     function onChange(e) {
    //         let value = e.target.value
    //         //node.value = moment(value).format('YYYY-MM-DDT00:00:00')
    //         console.log(value)
    //         node.value = moment(value).format('YYYY-MM-DDT00:00:00')
        
    //     }

    //     node.addEventListener('input', onChange )

    // } else {

    //     let value = node.innerHTML
    //     node.innerHTML = moment(value).format(format || 'DD.MM.YYYY')

    // }

}