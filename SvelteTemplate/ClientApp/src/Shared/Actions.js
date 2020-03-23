import {route} from '../store'
import moment from 'moment'
import { bind, assign, set_data, current_component, set_input_value, getContext  } from 'svelte/internal'

export function asDate(node, value) {
    
    let update = x => {
        node.innerHTML = x? moment(x).format('DD.MM.YYYY'): '';
    }

    return {
        update: update
    }

}

export function asRouterLink(node, className) {

    let unsubs = route.subscribe(x => {
        node.classList.toggle(className || "active", x.path.startsWith(node.getAttribute("href")));        
    })
    return {
        destroy: unsubs
    }
}


export function asSortLink(node, sort) {
    return {
    }
}



export function asLoader(node, loading) {
    

    let spinner = document.createElement('i');
    spinner.classList.add('fa');
    spinner.classList.add('fa-circle-o-notch');
    spinner.classList.add('fa-spin');

    spinner.classList.add('ml-2');

    let handleClick = e => {e.stopPropagation(); e.preventDefault()}

    let setLoading = x => {
        if (x) {
            node.appendChild(spinner)
            node.classList.add('disabled')
            node.addEventListener('click', handleClick)
        }
        else try {
            node.removeChild(spinner)
            node.classList.remove('disabled')
            node.removeEventListener('click', handleClick)
        } catch (error) {}
    }

    setLoading(loading)
    
    return {
        update: setLoading,
        destroy: x => {}
    }

}

export function validate(node, test) {

    let setValidate = x => {
        node.setCustomValidity(x?(node.getAttribute('message') || '?'):'');
    }

    setValidate(test);

    return {
        update: setValidate
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

export function bindDate(node, value) {


    node.type = "date";
    node.date = null
    

    //node.value = moment(value).format('YYYY-MM-DD');

    let update =  (x) =>  {
        let stringValue = x? moment(x).format('YYYY-MM-DD'): null;
        console.log(stringValue)
        node.value = stringValue
    };

    let onChange = (e) => {
        let dateValue = e.target.value? moment(e.target.value).format('YYYY-MM-DDT00:00:00'): null;
        console.log(dateValue)
        value = dateValue;
        //set_data(e.target, '111');
    }

    node.addEventListener('change', onChange )
    //update(value)


    return {
        update: update,
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